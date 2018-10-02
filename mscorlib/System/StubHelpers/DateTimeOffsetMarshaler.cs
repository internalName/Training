// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.DateTimeOffsetMarshaler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
  [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
  internal static class DateTimeOffsetMarshaler
  {
    private const long ManagedUtcTicksAtNativeZero = 504911232000000000;

    [SecurityCritical]
    internal static void ConvertToNative(ref DateTimeOffset managedDTO, out DateTimeNative dateTime)
    {
      long utcTicks = managedDTO.UtcTicks;
      dateTime.UniversalTime = utcTicks - 504911232000000000L;
    }

    [SecurityCritical]
    internal static void ConvertToManaged(out DateTimeOffset managedLocalDTO, ref DateTimeNative nativeTicks)
    {
      DateTimeOffset dateTimeOffset = new DateTimeOffset(504911232000000000L + nativeTicks.UniversalTime, TimeSpan.Zero);
      managedLocalDTO = dateTimeOffset.ToLocalTime(true);
    }
  }
}
