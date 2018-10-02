// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.SafeHeapHandleCache
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
  internal sealed class SafeHeapHandleCache : IDisposable
  {
    private readonly ulong _minSize;
    private readonly ulong _maxSize;
    [SecurityCritical]
    internal readonly SafeHeapHandle[] _handleCache;

    [SecuritySafeCritical]
    public SafeHeapHandleCache(ulong minSize = 64, ulong maxSize = 2048, int maxHandles = 0)
    {
      this._minSize = minSize;
      this._maxSize = maxSize;
      this._handleCache = new SafeHeapHandle[maxHandles > 0 ? maxHandles : Environment.ProcessorCount * 4];
    }

    [SecurityCritical]
    public SafeHeapHandle Acquire(ulong minSize = 0)
    {
      if (minSize < this._minSize)
        minSize = this._minSize;
      SafeHeapHandle safeHeapHandle = (SafeHeapHandle) null;
      for (int index = 0; index < this._handleCache.Length; ++index)
      {
        safeHeapHandle = Interlocked.Exchange<SafeHeapHandle>(ref this._handleCache[index], (SafeHeapHandle) null);
        if (safeHeapHandle != null)
          break;
      }
      if (safeHeapHandle != null)
      {
        if (safeHeapHandle.ByteLength < minSize)
          safeHeapHandle.Resize(minSize);
      }
      else
        safeHeapHandle = new SafeHeapHandle(minSize);
      return safeHeapHandle;
    }

    [SecurityCritical]
    public void Release(SafeHeapHandle handle)
    {
      if (handle.ByteLength <= this._maxSize)
      {
        for (int index = 0; index < this._handleCache.Length; ++index)
        {
          handle = Interlocked.Exchange<SafeHeapHandle>(ref this._handleCache[index], handle);
          if (handle == null)
            return;
        }
      }
      handle.Dispose();
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    [SecuritySafeCritical]
    private void Dispose(bool disposing)
    {
      if (this._handleCache == null)
        return;
      for (int index = 0; index < this._handleCache.Length; ++index)
      {
        SafeHeapHandle safeHeapHandle = this._handleCache[index];
        this._handleCache[index] = (SafeHeapHandle) null;
        if (safeHeapHandle != null & disposing)
          safeHeapHandle.Dispose();
      }
    }

    ~SafeHeapHandleCache()
    {
      this.Dispose(false);
    }
  }
}
