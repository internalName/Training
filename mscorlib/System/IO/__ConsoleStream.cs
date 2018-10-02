// Decompiled with JetBrains decompiler
// Type: System.IO.__ConsoleStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.IO
{
  internal sealed class __ConsoleStream : Stream
  {
    private const int BytesPerWChar = 2;
    [SecurityCritical]
    private SafeFileHandle _handle;
    private bool _canRead;
    private bool _canWrite;
    private bool _useFileAPIs;
    private bool _isPipe;

    [SecurityCritical]
    internal __ConsoleStream(SafeFileHandle handle, FileAccess access, bool useFileAPIs)
    {
      this._handle = handle;
      this._canRead = (access & FileAccess.Read) == FileAccess.Read;
      this._canWrite = (access & FileAccess.Write) == FileAccess.Write;
      this._useFileAPIs = useFileAPIs;
      this._isPipe = Win32Native.GetFileType(handle) == 3;
    }

    public override bool CanRead
    {
      get
      {
        return this._canRead;
      }
    }

    public override bool CanWrite
    {
      get
      {
        return this._canWrite;
      }
    }

    public override bool CanSeek
    {
      get
      {
        return false;
      }
    }

    public override long Length
    {
      get
      {
        __Error.SeekNotSupported();
        return 0;
      }
    }

    public override long Position
    {
      get
      {
        __Error.SeekNotSupported();
        return 0;
      }
      set
      {
        __Error.SeekNotSupported();
      }
    }

    [SecuritySafeCritical]
    protected override void Dispose(bool disposing)
    {
      if (this._handle != null)
        this._handle = (SafeFileHandle) null;
      this._canRead = false;
      this._canWrite = false;
      base.Dispose(disposing);
    }

    [SecuritySafeCritical]
    public override void Flush()
    {
      if (this._handle == null)
        __Error.FileNotOpen();
      if (this.CanWrite)
        return;
      __Error.WriteNotSupported();
    }

    public override void SetLength(long value)
    {
      __Error.SeekNotSupported();
    }

    [SecuritySafeCritical]
    public override int Read([In, Out] byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || count < 0)
        throw new ArgumentOutOfRangeException(offset < 0 ? nameof (offset) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (!this._canRead)
        __Error.ReadNotSupported();
      int bytesRead;
      int errorCode = __ConsoleStream.ReadFileNative(this._handle, buffer, offset, count, this._useFileAPIs, this._isPipe, out bytesRead);
      if (errorCode != 0)
        __Error.WinIOError(errorCode, string.Empty);
      return bytesRead;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      __Error.SeekNotSupported();
      return 0;
    }

    [SecuritySafeCritical]
    public override void Write(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0 || count < 0)
        throw new ArgumentOutOfRangeException(offset < 0 ? nameof (offset) : nameof (count), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (buffer.Length - offset < count)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      if (!this._canWrite)
        __Error.WriteNotSupported();
      int errorCode = __ConsoleStream.WriteFileNative(this._handle, buffer, offset, count, this._useFileAPIs);
      if (errorCode == 0)
        return;
      __Error.WinIOError(errorCode, string.Empty);
    }

    [SecurityCritical]
    private static unsafe int ReadFileNative(SafeFileHandle hFile, byte[] bytes, int offset, int count, bool useFileAPIs, bool isPipe, out int bytesRead)
    {
      if (bytes.Length - offset < count)
        throw new IndexOutOfRangeException(Environment.GetResourceString("IndexOutOfRange_IORaceCondition"));
      if (bytes.Length == 0)
      {
        bytesRead = 0;
        return 0;
      }
      __ConsoleStream.WaitForAvailableConsoleInput(hFile, isPipe);
      bool flag;
      if (useFileAPIs)
      {
        fixed (byte* numPtr = bytes)
          flag = (uint) Win32Native.ReadFile(hFile, numPtr + offset, count, out bytesRead, IntPtr.Zero) > 0U;
      }
      else
      {
        fixed (byte* numPtr = bytes)
        {
          int lpNumberOfCharsRead;
          flag = Win32Native.ReadConsoleW(hFile, numPtr + offset, count / 2, out lpNumberOfCharsRead, IntPtr.Zero);
          bytesRead = lpNumberOfCharsRead * 2;
        }
      }
      if (flag)
        return 0;
      int lastWin32Error = Marshal.GetLastWin32Error();
      switch (lastWin32Error)
      {
        case 109:
        case 232:
          return 0;
        default:
          return lastWin32Error;
      }
    }

    [SecurityCritical]
    private static unsafe int WriteFileNative(SafeFileHandle hFile, byte[] bytes, int offset, int count, bool useFileAPIs)
    {
      if (bytes.Length == 0)
        return 0;
      bool flag;
      if (useFileAPIs)
      {
        fixed (byte* numPtr = bytes)
        {
          int numBytesWritten;
          flag = (uint) Win32Native.WriteFile(hFile, numPtr + offset, count, out numBytesWritten, IntPtr.Zero) > 0U;
        }
      }
      else
      {
        fixed (byte* numPtr = bytes)
        {
          int lpNumberOfCharsWritten;
          flag = Win32Native.WriteConsoleW(hFile, numPtr + offset, count / 2, out lpNumberOfCharsWritten, IntPtr.Zero);
        }
      }
      if (flag)
        return 0;
      int lastWin32Error = Marshal.GetLastWin32Error();
      switch (lastWin32Error)
      {
        case 109:
        case 232:
          return 0;
        default:
          return lastWin32Error;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void WaitForAvailableConsoleInput(SafeFileHandle file, bool isPipe);
  }
}
