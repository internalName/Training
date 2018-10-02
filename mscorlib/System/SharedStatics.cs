// Decompiled with JetBrains decompiler
// Type: System.SharedStatics
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Util;
using System.Threading;

namespace System
{
  internal sealed class SharedStatics
  {
    private static SharedStatics _sharedStatics;
    private volatile string _Remoting_Identity_IDGuid;
    private Tokenizer.StringMaker _maker;
    private int _Remoting_Identity_IDSeqNum;
    private long _memFailPointReservedMemory;

    private SharedStatics()
    {
    }

    public static string Remoting_Identity_IDGuid
    {
      [SecuritySafeCritical] get
      {
        if (SharedStatics._sharedStatics._Remoting_Identity_IDGuid == null)
        {
          bool lockTaken = false;
          RuntimeHelpers.PrepareConstrainedRegions();
          try
          {
            Monitor.Enter((object) SharedStatics._sharedStatics, ref lockTaken);
            if (SharedStatics._sharedStatics._Remoting_Identity_IDGuid == null)
              SharedStatics._sharedStatics._Remoting_Identity_IDGuid = Guid.NewGuid().ToString().Replace('-', '_');
          }
          finally
          {
            if (lockTaken)
              Monitor.Exit((object) SharedStatics._sharedStatics);
          }
        }
        return SharedStatics._sharedStatics._Remoting_Identity_IDGuid;
      }
    }

    [SecuritySafeCritical]
    public static Tokenizer.StringMaker GetSharedStringMaker()
    {
      Tokenizer.StringMaker stringMaker = (Tokenizer.StringMaker) null;
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) SharedStatics._sharedStatics, ref lockTaken);
        if (SharedStatics._sharedStatics._maker != null)
        {
          stringMaker = SharedStatics._sharedStatics._maker;
          SharedStatics._sharedStatics._maker = (Tokenizer.StringMaker) null;
        }
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) SharedStatics._sharedStatics);
      }
      if (stringMaker == null)
        stringMaker = new Tokenizer.StringMaker();
      return stringMaker;
    }

    [SecuritySafeCritical]
    public static void ReleaseSharedStringMaker(ref Tokenizer.StringMaker maker)
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter((object) SharedStatics._sharedStatics, ref lockTaken);
        SharedStatics._sharedStatics._maker = maker;
        maker = (Tokenizer.StringMaker) null;
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit((object) SharedStatics._sharedStatics);
      }
    }

    internal static int Remoting_Identity_GetNextSeqNum()
    {
      return Interlocked.Increment(ref SharedStatics._sharedStatics._Remoting_Identity_IDSeqNum);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static long AddMemoryFailPointReservation(long size)
    {
      return Interlocked.Add(ref SharedStatics._sharedStatics._memFailPointReservedMemory, size);
    }

    internal static ulong MemoryFailPointReservedMemory
    {
      get
      {
        return (ulong) Volatile.Read(ref SharedStatics._sharedStatics._memFailPointReservedMemory);
      }
    }
  }
}
