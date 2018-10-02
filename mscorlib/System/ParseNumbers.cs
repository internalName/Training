﻿// Decompiled with JetBrains decompiler
// Type: System.ParseNumbers
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
  internal static class ParseNumbers
  {
    internal const int PrintAsI1 = 64;
    internal const int PrintAsI2 = 128;
    internal const int PrintAsI4 = 256;
    internal const int TreatAsUnsigned = 512;
    internal const int TreatAsI1 = 1024;
    internal const int TreatAsI2 = 2048;
    internal const int IsTight = 4096;
    internal const int NoSpace = 8192;

    [SecuritySafeCritical]
    public static unsafe long StringToLong(string s, int radix, int flags)
    {
      return ParseNumbers.StringToLong(s, radix, flags, (int*) null);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern unsafe long StringToLong(string s, int radix, int flags, int* currPos);

    [SecuritySafeCritical]
    public static unsafe long StringToLong(string s, int radix, int flags, ref int currPos)
    {
      fixed (int* currPos1 = &currPos)
        return ParseNumbers.StringToLong(s, radix, flags, currPos1);
    }

    [SecuritySafeCritical]
    public static unsafe int StringToInt(string s, int radix, int flags)
    {
      return ParseNumbers.StringToInt(s, radix, flags, (int*) null);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern unsafe int StringToInt(string s, int radix, int flags, int* currPos);

    [SecuritySafeCritical]
    public static unsafe int StringToInt(string s, int radix, int flags, ref int currPos)
    {
      fixed (int* currPos1 = &currPos)
        return ParseNumbers.StringToInt(s, radix, flags, currPos1);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern string IntToString(int l, int radix, int width, char paddingChar, int flags);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern string LongToString(long l, int radix, int width, char paddingChar, int flags);
  }
}
