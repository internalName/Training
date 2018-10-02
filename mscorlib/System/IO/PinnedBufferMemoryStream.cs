// Decompiled with JetBrains decompiler
// Type: System.IO.PinnedBufferMemoryStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.IO
{
  internal sealed class PinnedBufferMemoryStream : UnmanagedMemoryStream
  {
    private byte[] _array;
    private GCHandle _pinningHandle;

    [SecurityCritical]
    private PinnedBufferMemoryStream()
    {
    }

    [SecurityCritical]
    internal unsafe PinnedBufferMemoryStream(byte[] array)
    {
      int num = array.Length;
      if (num == 0)
      {
        array = new byte[1];
        num = 0;
      }
      this._array = array;
      this._pinningHandle = new GCHandle((object) array, GCHandleType.Pinned);
      fixed (byte* pointer = this._array)
        this.Initialize(pointer, (long) num, (long) num, FileAccess.Read, true);
    }

    ~PinnedBufferMemoryStream()
    {
      this.Dispose(false);
    }

    [SecuritySafeCritical]
    protected override void Dispose(bool disposing)
    {
      if (this._isOpen)
      {
        this._pinningHandle.Free();
        this._isOpen = false;
      }
      base.Dispose(disposing);
    }
  }
}
