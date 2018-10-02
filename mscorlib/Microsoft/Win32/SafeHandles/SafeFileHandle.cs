// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeFileHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  /// <summary>Представляет класс-оболочку для дескриптора файла.</summary>
  [SecurityCritical]
  public sealed class SafeFileHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafeFileHandle()
      : base(true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" />.
    /// </summary>
    /// <param name="preexistingHandle">
    ///   Объект <see cref="T:System.IntPtr" />, представляющий ранее существующий дескриптор для использования.
    /// </param>
    /// <param name="ownsHandle">
    ///   Значение <see langword="true" />, чтобы надежно освободить маркер на стадии завершения; значение <see langword="false" />, чтобы предотвратить надежное освобождение (не рекомендуется).
    /// </param>
    public SafeFileHandle(IntPtr preexistingHandle, bool ownsHandle)
      : base(ownsHandle)
    {
      this.SetHandle(preexistingHandle);
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return Win32Native.CloseHandle(this.handle);
    }
  }
}
