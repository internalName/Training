// Decompiled with JetBrains decompiler
// Type: System.Threading.CompressedStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет методы для установки и инициализации сжатого стека в текущем потоке.
  ///    Этот класс не наследуется.
  /// </summary>
  [Serializable]
  public sealed class CompressedStack : ISerializable
  {
    private volatile PermissionListSet m_pls;
    [SecurityCritical]
    private volatile SafeCompressedStackHandle m_csHandle;
    private bool m_canSkipEvaluation;
    internal static volatile RuntimeHelpers.TryCode tryCode;
    internal static volatile RuntimeHelpers.CleanupCode cleanupCode;

    internal bool CanSkipEvaluation
    {
      get
      {
        return this.m_canSkipEvaluation;
      }
      private set
      {
        this.m_canSkipEvaluation = value;
      }
    }

    internal PermissionListSet PLS
    {
      get
      {
        return this.m_pls;
      }
    }

    [SecurityCritical]
    internal CompressedStack(SafeCompressedStackHandle csHandle)
    {
      this.m_csHandle = csHandle;
    }

    [SecurityCritical]
    private CompressedStack(SafeCompressedStackHandle csHandle, PermissionListSet pls)
    {
      this.m_csHandle = csHandle;
      this.m_pls = pls;
    }

    /// <summary>
    ///   Задает объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> с информацией логического контекста, необходимой для повторного создания экземпляра данного контекста выполнения.
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
      this.CompleteConstruction((CompressedStack) null);
      info.AddValue("PLS", (object) this.m_pls);
    }

    private CompressedStack(SerializationInfo info, StreamingContext context)
    {
      this.m_pls = (PermissionListSet) info.GetValue(nameof (PLS), typeof (PermissionListSet));
    }

    internal SafeCompressedStackHandle CompressedStackHandle
    {
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.m_csHandle;
      }
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] private set
      {
        this.m_csHandle = value;
      }
    }

    /// <summary>Возвращает сжатый стек для текущего потока.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.CompressedStack" /> для текущего потока.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего кода в цепочке вызовов не имеет разрешения на доступ к неуправляемому коду.
    /// 
    ///   -или-
    /// 
    ///   Запрос <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> не удалось.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static CompressedStack GetCompressedStack()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return CompressedStack.GetCompressedStack(ref stackMark);
    }

    [SecurityCritical]
    internal static CompressedStack GetCompressedStack(ref StackCrawlMark stackMark)
    {
      CompressedStack innerCS = (CompressedStack) null;
      CompressedStack compressedStack;
      if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
      {
        compressedStack = new CompressedStack((SafeCompressedStackHandle) null);
        compressedStack.CanSkipEvaluation = true;
      }
      else if (CodeAccessSecurityEngine.AllDomainsHomogeneousWithNoStackModifiers())
      {
        compressedStack = new CompressedStack(CompressedStack.GetDelayedCompressedStack(ref stackMark, false));
        compressedStack.m_pls = PermissionListSet.CreateCompressedState_HG();
      }
      else
      {
        compressedStack = new CompressedStack((SafeCompressedStackHandle) null);
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          compressedStack.CompressedStackHandle = CompressedStack.GetDelayedCompressedStack(ref stackMark, true);
          if (compressedStack.CompressedStackHandle != null)
          {
            if (CompressedStack.IsImmediateCompletionCandidate(compressedStack.CompressedStackHandle, out innerCS))
            {
              try
              {
                compressedStack.CompleteConstruction(innerCS);
              }
              finally
              {
                CompressedStack.DestroyDCSList(compressedStack.CompressedStackHandle);
              }
            }
          }
        }
      }
      return compressedStack;
    }

    /// <summary>Перехватывает сжатый стек из текущего потока.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.CompressedStack" />.
    /// </returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static CompressedStack Capture()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return CompressedStack.GetCompressedStack(ref stackMark);
    }

    /// <summary>
    ///   Выполняет метод в заданном сжатом стеке в текущем потоке.
    /// </summary>
    /// <param name="compressedStack">
    ///   Записываемая задача <see cref="T:System.Threading.CompressedStack" />.
    /// </param>
    /// <param name="callback">
    ///   Объект <see cref="T:System.Threading.ContextCallback" /> представляющий метод, выполняемый в указанном контексте безопасности.
    /// </param>
    /// <param name="state">
    ///   Объект, передаваемый в метод обратного вызова.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="compressedStack" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void Run(CompressedStack compressedStack, ContextCallback callback, object state)
    {
      if (compressedStack == null)
        throw new ArgumentException(Environment.GetResourceString("Arg_NamedParamNull"), nameof (compressedStack));
      if (CompressedStack.cleanupCode == null)
      {
        CompressedStack.tryCode = new RuntimeHelpers.TryCode(CompressedStack.runTryCode);
        CompressedStack.cleanupCode = new RuntimeHelpers.CleanupCode(CompressedStack.runFinallyCode);
      }
      CompressedStack.CompressedStackRunData compressedStackRunData = new CompressedStack.CompressedStackRunData(compressedStack, callback, state);
      RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(CompressedStack.tryCode, CompressedStack.cleanupCode, (object) compressedStackRunData);
    }

    [SecurityCritical]
    internal static void runTryCode(object userData)
    {
      CompressedStack.CompressedStackRunData compressedStackRunData = (CompressedStack.CompressedStackRunData) userData;
      compressedStackRunData.cssw = CompressedStack.SetCompressedStack(compressedStackRunData.cs, CompressedStack.GetCompressedStackThread());
      compressedStackRunData.callBack(compressedStackRunData.state);
    }

    [SecurityCritical]
    [PrePrepareMethod]
    internal static void runFinallyCode(object userData, bool exceptionThrown)
    {
      ((CompressedStack.CompressedStackRunData) userData).cssw.Undo();
    }

    [SecurityCritical]
    [HandleProcessCorruptedStateExceptions]
    internal static CompressedStackSwitcher SetCompressedStack(CompressedStack cs, CompressedStack prevCS)
    {
      CompressedStackSwitcher compressedStackSwitcher = new CompressedStackSwitcher();
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          CompressedStack.SetCompressedStackThread(cs);
          compressedStackSwitcher.prev_CS = prevCS;
          compressedStackSwitcher.curr_CS = cs;
          compressedStackSwitcher.prev_ADStack = CompressedStack.SetAppDomainStack(cs);
        }
      }
      catch
      {
        compressedStackSwitcher.UndoNoThrow();
        throw;
      }
      return compressedStackSwitcher;
    }

    /// <summary>Создает копию текущего сжатого стека.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.CompressedStack" /> объект, представляющий текущий сжатый стек.
    /// </returns>
    [SecuritySafeCritical]
    [ComVisible(false)]
    public CompressedStack CreateCopy()
    {
      return new CompressedStack(this.m_csHandle, this.m_pls);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static IntPtr SetAppDomainStack(CompressedStack cs)
    {
      return Thread.CurrentThread.SetAppDomainStack(cs == null ? (SafeCompressedStackHandle) null : cs.CompressedStackHandle);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static void RestoreAppDomainStack(IntPtr appDomainStack)
    {
      Thread.CurrentThread.RestoreAppDomainStack(appDomainStack);
    }

    [SecurityCritical]
    internal static CompressedStack GetCompressedStackThread()
    {
      return Thread.CurrentThread.GetExecutionContextReader().SecurityContext.CompressedStack;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal static void SetCompressedStackThread(CompressedStack cs)
    {
      Thread currentThread = Thread.CurrentThread;
      if (currentThread.GetExecutionContextReader().SecurityContext.CompressedStack == cs)
        return;
      ExecutionContext executionContext = currentThread.GetMutableExecutionContext();
      if (executionContext.SecurityContext != null)
      {
        executionContext.SecurityContext.CompressedStack = cs;
      }
      else
      {
        if (cs == null)
          return;
        executionContext.SecurityContext = new SecurityContext()
        {
          CompressedStack = cs
        };
      }
    }

    [SecurityCritical]
    internal bool CheckDemand(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
    {
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return false;
      this.PLS.CheckDemand(demand, permToken, rmh);
      return false;
    }

    [SecurityCritical]
    internal bool CheckDemandNoHalt(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
    {
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return true;
      return this.PLS.CheckDemand(demand, permToken, rmh);
    }

    [SecurityCritical]
    internal bool CheckSetDemand(PermissionSet pset, RuntimeMethodHandleInternal rmh)
    {
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return false;
      return this.PLS.CheckSetDemand(pset, rmh);
    }

    [SecurityCritical]
    internal bool CheckSetDemandWithModificationNoHalt(PermissionSet pset, out PermissionSet alteredDemandSet, RuntimeMethodHandleInternal rmh)
    {
      alteredDemandSet = (PermissionSet) null;
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return true;
      return this.PLS.CheckSetDemandWithModification(pset, out alteredDemandSet, rmh);
    }

    [SecurityCritical]
    internal void DemandFlagsOrGrantSet(int flags, PermissionSet grantSet)
    {
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return;
      this.PLS.DemandFlagsOrGrantSet(flags, grantSet);
    }

    [SecurityCritical]
    internal void GetZoneAndOrigin(ArrayList zoneList, ArrayList originList, PermissionToken zoneToken, PermissionToken originToken)
    {
      this.CompleteConstruction((CompressedStack) null);
      if (this.PLS == null)
        return;
      this.PLS.GetZoneAndOrigin(zoneList, originList, zoneToken, originToken);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    internal void CompleteConstruction(CompressedStack innerCS)
    {
      if (this.PLS != null)
        return;
      PermissionListSet compressedState = PermissionListSet.CreateCompressedState(this, innerCS);
      lock (this)
      {
        if (this.PLS != null)
          return;
        this.m_pls = compressedState;
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern SafeCompressedStackHandle GetDelayedCompressedStack(ref StackCrawlMark stackMark, bool walkStack);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void DestroyDelayedCompressedStack(IntPtr unmanagedCompressedStack);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void DestroyDCSList(SafeCompressedStackHandle compressedStack);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetDCSCount(SafeCompressedStackHandle compressedStack);

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsImmediateCompletionCandidate(SafeCompressedStackHandle compressedStack, out CompressedStack innerCS);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern DomainCompressedStack GetDomainCompressedStack(SafeCompressedStackHandle compressedStack, int index);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void GetHomogeneousPLS(PermissionListSet hgPLS);

    internal class CompressedStackRunData
    {
      internal CompressedStack cs;
      internal ContextCallback callBack;
      internal object state;
      internal CompressedStackSwitcher cssw;

      internal CompressedStackRunData(CompressedStack cs, ContextCallback cb, object state)
      {
        this.cs = cs;
        this.callBack = cb;
        this.state = state;
        this.cssw = new CompressedStackSwitcher();
      }
    }
  }
}
