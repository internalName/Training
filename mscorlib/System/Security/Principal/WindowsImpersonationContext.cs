// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.WindowsImpersonationContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>
  ///   Представляет пользователя Windows до операции олицетворения.
  /// </summary>
  [ComVisible(true)]
  public class WindowsImpersonationContext : IDisposable
  {
    [SecurityCritical]
    private SafeAccessTokenHandle m_safeTokenHandle = SafeAccessTokenHandle.InvalidHandle;
    private WindowsIdentity m_wi;
    private FrameSecurityDescriptor m_fsd;

    [SecurityCritical]
    private WindowsImpersonationContext()
    {
    }

    [SecurityCritical]
    internal WindowsImpersonationContext(SafeAccessTokenHandle safeTokenHandle, WindowsIdentity wi, bool isImpersonating, FrameSecurityDescriptor fsd)
    {
      if (safeTokenHandle.IsInvalid)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
      if (isImpersonating)
      {
        if (!Win32Native.DuplicateHandle(Win32Native.GetCurrentProcess(), safeTokenHandle, Win32Native.GetCurrentProcess(), ref this.m_safeTokenHandle, 0U, true, 2U))
          throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
        this.m_wi = wi;
      }
      this.m_fsd = fsd;
    }

    /// <summary>
    ///   Возвращает контекст пользователя пользователю Windows, представленному данным объектом.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Попытка использования этого метода для любых целей, кроме возврата удостоверения в исходное состояние.
    /// </exception>
    [SecuritySafeCritical]
    public void Undo()
    {
      if (this.m_safeTokenHandle.IsInvalid)
      {
        int self = System.Security.Principal.Win32.RevertToSelf();
        if (self < 0)
          Environment.FailFast(Win32Native.GetMessage(self));
      }
      else
      {
        int self = System.Security.Principal.Win32.RevertToSelf();
        if (self < 0)
          Environment.FailFast(Win32Native.GetMessage(self));
        int errorCode = System.Security.Principal.Win32.ImpersonateLoggedOnUser(this.m_safeTokenHandle);
        if (errorCode < 0)
          throw new SecurityException(Win32Native.GetMessage(errorCode));
      }
      WindowsIdentity.UpdateThreadWI(this.m_wi);
      if (this.m_fsd == null)
        return;
      this.m_fsd.SetTokenHandles((SafeAccessTokenHandle) null, (SafeAccessTokenHandle) null);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [HandleProcessCorruptedStateExceptions]
    internal bool UndoNoThrow()
    {
      bool flag;
      try
      {
        int errorCode;
        if (this.m_safeTokenHandle.IsInvalid)
        {
          errorCode = System.Security.Principal.Win32.RevertToSelf();
          if (errorCode < 0)
            Environment.FailFast(Win32Native.GetMessage(errorCode));
        }
        else
        {
          errorCode = System.Security.Principal.Win32.RevertToSelf();
          if (errorCode >= 0)
            errorCode = System.Security.Principal.Win32.ImpersonateLoggedOnUser(this.m_safeTokenHandle);
          else
            Environment.FailFast(Win32Native.GetMessage(errorCode));
        }
        flag = errorCode >= 0;
        if (this.m_fsd != null)
          this.m_fsd.SetTokenHandles((SafeAccessTokenHandle) null, (SafeAccessTokenHandle) null);
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Security.Principal.WindowsImpersonationContext" />, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [SecuritySafeCritical]
    [ComVisible(false)]
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.m_safeTokenHandle == null || this.m_safeTokenHandle.IsClosed)
        return;
      this.Undo();
      this.m_safeTokenHandle.Dispose();
    }

    /// <summary>
    ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.Security.Principal.WindowsImpersonationContext" />.
    /// </summary>
    [ComVisible(false)]
    public void Dispose()
    {
      this.Dispose(true);
    }
  }
}
