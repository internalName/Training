// Decompiled with JetBrains decompiler
// Type: System.Threading.DeferredDisposableLifetime`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  internal struct DeferredDisposableLifetime<T> where T : class, IDeferredDisposable
  {
    private int _count;

    public bool AddRef(T obj)
    {
      int comparand;
      do
      {
        comparand = Volatile.Read(ref this._count);
        if (comparand < 0)
          throw new ObjectDisposedException(typeof (T).ToString());
      }
      while (Interlocked.CompareExchange(ref this._count, checked (comparand + 1), comparand) != comparand);
      return true;
    }

    [SecurityCritical]
    public void Release(T obj)
    {
      int comparand;
      int num1;
      do
      {
        comparand = Volatile.Read(ref this._count);
        if (comparand > 0)
        {
          int num2 = comparand - 1;
          if (Interlocked.CompareExchange(ref this._count, num2, comparand) == comparand)
          {
            if (num2 != 0)
              return;
            obj.OnFinalRelease(false);
            return;
          }
        }
        else
          num1 = comparand + 1;
      }
      while (Interlocked.CompareExchange(ref this._count, num1, comparand) != comparand);
      if (num1 != -1)
        return;
      obj.OnFinalRelease(true);
    }

    [SecuritySafeCritical]
    public void Dispose(T obj)
    {
      int comparand;
      int num;
      do
      {
        comparand = Volatile.Read(ref this._count);
        if (comparand < 0)
          return;
        num = -1 - comparand;
      }
      while (Interlocked.CompareExchange(ref this._count, num, comparand) != comparand);
      if (num != -1)
        return;
      obj.OnFinalRelease(true);
    }
  }
}
