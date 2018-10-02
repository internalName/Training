// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.Privilege
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
  internal sealed class Privilege
  {
    private static LocalDataStoreSlot tlsSlot = Thread.AllocateDataSlot();
    private static Hashtable privileges = new Hashtable();
    private static Hashtable luids = new Hashtable();
    private static ReaderWriterLock privilegeLock = new ReaderWriterLock();
    private readonly Thread currentThread = Thread.CurrentThread;
    private bool needToRevert;
    private bool initialState;
    private bool stateWasChanged;
    [SecurityCritical]
    private Win32Native.LUID luid;
    private Privilege.TlsContents tlsContents;
    public const string CreateToken = "SeCreateTokenPrivilege";
    public const string AssignPrimaryToken = "SeAssignPrimaryTokenPrivilege";
    public const string LockMemory = "SeLockMemoryPrivilege";
    public const string IncreaseQuota = "SeIncreaseQuotaPrivilege";
    public const string UnsolicitedInput = "SeUnsolicitedInputPrivilege";
    public const string MachineAccount = "SeMachineAccountPrivilege";
    public const string TrustedComputingBase = "SeTcbPrivilege";
    public const string Security = "SeSecurityPrivilege";
    public const string TakeOwnership = "SeTakeOwnershipPrivilege";
    public const string LoadDriver = "SeLoadDriverPrivilege";
    public const string SystemProfile = "SeSystemProfilePrivilege";
    public const string SystemTime = "SeSystemtimePrivilege";
    public const string ProfileSingleProcess = "SeProfileSingleProcessPrivilege";
    public const string IncreaseBasePriority = "SeIncreaseBasePriorityPrivilege";
    public const string CreatePageFile = "SeCreatePagefilePrivilege";
    public const string CreatePermanent = "SeCreatePermanentPrivilege";
    public const string Backup = "SeBackupPrivilege";
    public const string Restore = "SeRestorePrivilege";
    public const string Shutdown = "SeShutdownPrivilege";
    public const string Debug = "SeDebugPrivilege";
    public const string Audit = "SeAuditPrivilege";
    public const string SystemEnvironment = "SeSystemEnvironmentPrivilege";
    public const string ChangeNotify = "SeChangeNotifyPrivilege";
    public const string RemoteShutdown = "SeRemoteShutdownPrivilege";
    public const string Undock = "SeUndockPrivilege";
    public const string SyncAgent = "SeSyncAgentPrivilege";
    public const string EnableDelegation = "SeEnableDelegationPrivilege";
    public const string ManageVolume = "SeManageVolumePrivilege";
    public const string Impersonate = "SeImpersonatePrivilege";
    public const string CreateGlobal = "SeCreateGlobalPrivilege";
    public const string TrustedCredentialManagerAccess = "SeTrustedCredManAccessPrivilege";
    public const string ReserveProcessor = "SeReserveProcessorPrivilege";

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private static Win32Native.LUID LuidFromPrivilege(string privilege)
    {
      Win32Native.LUID luid;
      luid.LowPart = 0U;
      luid.HighPart = 0U;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Privilege.privilegeLock.AcquireReaderLock(-1);
        if (Privilege.luids.Contains((object) privilege))
        {
          luid = (Win32Native.LUID) Privilege.luids[(object) privilege];
          Privilege.privilegeLock.ReleaseReaderLock();
        }
        else
        {
          Privilege.privilegeLock.ReleaseReaderLock();
          if (!Win32Native.LookupPrivilegeValue((string) null, privilege, ref luid))
          {
            switch (Marshal.GetLastWin32Error())
            {
              case 5:
                throw new UnauthorizedAccessException();
              case 8:
                throw new OutOfMemoryException();
              case 1313:
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPrivilegeName", (object) privilege));
              default:
                throw new InvalidOperationException();
            }
          }
          else
            Privilege.privilegeLock.AcquireWriterLock(-1);
        }
      }
      finally
      {
        if (Privilege.privilegeLock.IsReaderLockHeld)
          Privilege.privilegeLock.ReleaseReaderLock();
        if (Privilege.privilegeLock.IsWriterLockHeld)
        {
          if (!Privilege.luids.Contains((object) privilege))
          {
            Privilege.luids[(object) privilege] = (object) luid;
            Privilege.privileges[(object) luid] = (object) privilege;
          }
          Privilege.privilegeLock.ReleaseWriterLock();
        }
      }
      return luid;
    }

    [SecurityCritical]
    public Privilege(string privilegeName)
    {
      if (privilegeName == null)
        throw new ArgumentNullException(nameof (privilegeName));
      this.luid = Privilege.LuidFromPrivilege(privilegeName);
    }

    [SecuritySafeCritical]
    ~Privilege()
    {
      if (!this.needToRevert)
        return;
      this.Revert();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public void Enable()
    {
      this.ToggleState(true);
    }

    public bool NeedToRevert
    {
      get
      {
        return this.needToRevert;
      }
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    private void ToggleState(bool enable)
    {
      int num = 0;
      if (!this.currentThread.Equals((object) Thread.CurrentThread))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustBeSameThread"));
      if (this.needToRevert)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustRevertPrivilege"));
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        try
        {
          this.tlsContents = Thread.GetData(Privilege.tlsSlot) as Privilege.TlsContents;
          if (this.tlsContents == null)
          {
            this.tlsContents = new Privilege.TlsContents();
            Thread.SetData(Privilege.tlsSlot, (object) this.tlsContents);
          }
          else
            this.tlsContents.IncrementReferenceCount();
          Win32Native.TOKEN_PRIVILEGE NewState = new Win32Native.TOKEN_PRIVILEGE();
          NewState.PrivilegeCount = 1U;
          NewState.Privilege.Luid = this.luid;
          NewState.Privilege.Attributes = enable ? 2U : 0U;
          Win32Native.TOKEN_PRIVILEGE PreviousState = new Win32Native.TOKEN_PRIVILEGE();
          uint ReturnLength = 0;
          if (!Win32Native.AdjustTokenPrivileges(this.tlsContents.ThreadHandle, false, ref NewState, (uint) Marshal.SizeOf<Win32Native.TOKEN_PRIVILEGE>(PreviousState), ref PreviousState, ref ReturnLength))
            num = Marshal.GetLastWin32Error();
          else if (1300 == Marshal.GetLastWin32Error())
          {
            num = 1300;
          }
          else
          {
            this.initialState = (PreviousState.Privilege.Attributes & 2U) > 0U;
            this.stateWasChanged = this.initialState != enable;
            this.needToRevert = this.tlsContents.IsImpersonating || this.stateWasChanged;
          }
        }
        finally
        {
          if (!this.needToRevert)
            this.Reset();
        }
      }
      if (num == 1300)
        throw new PrivilegeNotHeldException(Privilege.privileges[(object) this.luid] as string);
      if (num == 8)
        throw new OutOfMemoryException();
      if (num == 5 || num == 1347)
        throw new UnauthorizedAccessException();
      if (num != 0)
        throw new InvalidOperationException();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public void Revert()
    {
      int num = 0;
      if (!this.currentThread.Equals((object) Thread.CurrentThread))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustBeSameThread"));
      if (!this.NeedToRevert)
        return;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        bool flag = true;
        try
        {
          if (this.stateWasChanged)
          {
            if (this.tlsContents.ReferenceCountValue <= 1)
            {
              if (this.tlsContents.IsImpersonating)
                goto label_16;
            }
            Win32Native.TOKEN_PRIVILEGE NewState = new Win32Native.TOKEN_PRIVILEGE();
            NewState.PrivilegeCount = 1U;
            NewState.Privilege.Luid = this.luid;
            NewState.Privilege.Attributes = this.initialState ? 2U : 0U;
            Win32Native.TOKEN_PRIVILEGE PreviousState = new Win32Native.TOKEN_PRIVILEGE();
            uint ReturnLength = 0;
            if (!Win32Native.AdjustTokenPrivileges(this.tlsContents.ThreadHandle, false, ref NewState, (uint) Marshal.SizeOf<Win32Native.TOKEN_PRIVILEGE>(PreviousState), ref PreviousState, ref ReturnLength))
            {
              num = Marshal.GetLastWin32Error();
              flag = false;
            }
          }
        }
        finally
        {
          if (flag)
            this.Reset();
        }
label_16:;
      }
      if (num == 8)
        throw new OutOfMemoryException();
      if (num == 5)
        throw new UnauthorizedAccessException();
      if (num != 0)
        throw new InvalidOperationException();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private void Reset()
    {
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        this.stateWasChanged = false;
        this.initialState = false;
        this.needToRevert = false;
        if (this.tlsContents != null && this.tlsContents.DecrementReferenceCount() == 0)
        {
          this.tlsContents = (Privilege.TlsContents) null;
          Thread.SetData(Privilege.tlsSlot, (object) null);
        }
      }
    }

    private sealed class TlsContents : IDisposable
    {
      private int referenceCount = 1;
      [SecurityCritical]
      private SafeAccessTokenHandle threadHandle = new SafeAccessTokenHandle(IntPtr.Zero);
      [SecurityCritical]
      private static volatile SafeAccessTokenHandle processHandle = new SafeAccessTokenHandle(IntPtr.Zero);
      private static readonly object syncRoot = new object();
      private bool disposed;
      private bool isImpersonating;

      [SecuritySafeCritical]
      static TlsContents()
      {
      }

      [SecurityCritical]
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
      public TlsContents()
      {
        int num1 = 0;
        int num2 = 0;
        bool flag = true;
        if (Privilege.TlsContents.processHandle.IsInvalid)
        {
          lock (Privilege.TlsContents.syncRoot)
          {
            if (Privilege.TlsContents.processHandle.IsInvalid)
            {
              SafeAccessTokenHandle TokenHandle;
              if (!Win32Native.OpenProcessToken(Win32Native.GetCurrentProcess(), TokenAccessLevels.Duplicate, out TokenHandle))
              {
                num2 = Marshal.GetLastWin32Error();
                flag = false;
              }
              Privilege.TlsContents.processHandle = TokenHandle;
            }
          }
        }
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          try
          {
            SafeAccessTokenHandle threadHandle = this.threadHandle;
            num1 = System.Security.Principal.Win32.OpenThreadToken(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, WinSecurityContext.Process, out this.threadHandle);
            num1 &= 2147024895;
            if (num1 != 0)
            {
              if (flag)
              {
                this.threadHandle = threadHandle;
                if (num1 != 1008)
                  flag = false;
                if (flag)
                {
                  num1 = 0;
                  if (!Win32Native.DuplicateTokenEx(Privilege.TlsContents.processHandle, TokenAccessLevels.Impersonate | TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, IntPtr.Zero, Win32Native.SECURITY_IMPERSONATION_LEVEL.Impersonation, System.Security.Principal.TokenType.TokenImpersonation, ref this.threadHandle))
                  {
                    num1 = Marshal.GetLastWin32Error();
                    flag = false;
                  }
                }
                if (flag)
                {
                  num1 = System.Security.Principal.Win32.SetThreadToken(this.threadHandle);
                  num1 &= 2147024895;
                  if (num1 != 0)
                    flag = false;
                }
                if (flag)
                  this.isImpersonating = true;
              }
              else
                num1 = num2;
            }
            else
              flag = true;
          }
          finally
          {
            if (!flag)
              this.Dispose();
          }
        }
        if (num1 == 8)
          throw new OutOfMemoryException();
        if (num1 == 5 || num1 == 1347)
          throw new UnauthorizedAccessException();
        if (num1 != 0)
          throw new InvalidOperationException();
      }

      [SecuritySafeCritical]
      ~TlsContents()
      {
        if (this.disposed)
          return;
        this.Dispose(false);
      }

      [SecuritySafeCritical]
      public void Dispose()
      {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
      }

      [SecurityCritical]
      private void Dispose(bool disposing)
      {
        if (this.disposed)
          return;
        if (disposing && this.threadHandle != null)
        {
          this.threadHandle.Dispose();
          this.threadHandle = (SafeAccessTokenHandle) null;
        }
        if (this.isImpersonating)
          System.Security.Principal.Win32.RevertToSelf();
        this.disposed = true;
      }

      public void IncrementReferenceCount()
      {
        ++this.referenceCount;
      }

      [SecurityCritical]
      public int DecrementReferenceCount()
      {
        int num = --this.referenceCount;
        if (num == 0)
          this.Dispose();
        return num;
      }

      public int ReferenceCountValue
      {
        get
        {
          return this.referenceCount;
        }
      }

      public SafeAccessTokenHandle ThreadHandle
      {
        [SecurityCritical] get
        {
          return this.threadHandle;
        }
      }

      public bool IsImpersonating
      {
        get
        {
          return this.isImpersonating;
        }
      }
    }
  }
}
