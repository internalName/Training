// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.SafeHandles.SafeAccessTokenHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
  /// <summary>
  ///   Предоставляет безопасный дескриптор для потока Windows или маркера доступа процесса.
  ///    Дополнительные сведения см. в разделеAccess Tokens
  /// </summary>
  [SecurityCritical]
  public sealed class SafeAccessTokenHandle : SafeHandle
  {
    private SafeAccessTokenHandle()
      : base(IntPtr.Zero, true)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" />.
    /// </summary>
    /// <param name="handle">
    ///   Объект <see cref="T:System.IntPtr" />, представляющий ранее существующий дескриптор для использования.
    ///    Используя <see cref="F:System.IntPtr.Zero" />, возвращает недопустимый дескриптор.
    /// </param>
    public SafeAccessTokenHandle(IntPtr handle)
      : base(IntPtr.Zero, true)
    {
      this.SetHandle(handle);
    }

    /// <summary>
    ///   Возвращает недопустимый дескриптор путем создания экземпляра объекта <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" /> с <see cref="F:System.IntPtr.Zero" />.
    /// </summary>
    /// <returns>
    ///   Возвращает объект <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" />.
    /// </returns>
    public static SafeAccessTokenHandle InvalidHandle
    {
      [SecurityCritical] get
      {
        return new SafeAccessTokenHandle(IntPtr.Zero);
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, является ли дескриптор недействительным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если дескриптор недействителен, в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsInvalid
    {
      [SecurityCritical] get
      {
        if (!(this.handle == IntPtr.Zero))
          return this.handle == new IntPtr(-1);
        return true;
      }
    }

    [SecurityCritical]
    protected override bool ReleaseHandle()
    {
      return Win32Native.CloseHandle(this.handle);
    }
  }
}
