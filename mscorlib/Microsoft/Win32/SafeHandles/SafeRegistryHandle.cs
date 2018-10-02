// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeRegistryHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  /// <summary>
  ///   Представляет безопасный дескриптор для реестра Windows.
  /// </summary>
  [SecurityCritical]
  public sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    [SecurityCritical]
    internal SafeRegistryHandle()
      : base(true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:Microsoft.Win32.SafeHandles.SafeRegistryHandle" />.
    /// </summary>
    /// <param name="preexistingHandle">
    ///   Объект, представляющий уже существовавший ранее дескриптор для использования.
    /// </param>
    /// <param name="ownsHandle">
    ///   Значение <see langword="true" />, чтобы надежно освободить дескриптор на стадии завершения; значение <see langword="false" />, чтобы запретить надежное освобождение.
    /// </param>
    [SecurityCritical]
    public SafeRegistryHandle(IntPtr preexistingHandle, bool ownsHandle)
      : base(ownsHandle)
    {
      this.SetHandle(preexistingHandle);
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return SafeRegistryHandle.RegCloseKey(this.handle) == 0;
    }

    [SuppressUnmanagedCodeSecurity]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("advapi32.dll")]
    internal static extern int RegCloseKey(IntPtr hKey);
  }
}
