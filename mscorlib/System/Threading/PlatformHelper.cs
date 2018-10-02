// Decompiled with JetBrains decompiler
// Type: System.Threading.PlatformHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  internal static class PlatformHelper
  {
    private const int PROCESSOR_COUNT_REFRESH_INTERVAL_MS = 30000;
    private static volatile int s_processorCount;
    private static volatile int s_lastProcessorCountRefreshTicks;

    internal static int ProcessorCount
    {
      get
      {
        int tickCount = Environment.TickCount;
        int processorCount = PlatformHelper.s_processorCount;
        if (processorCount == 0 || tickCount - PlatformHelper.s_lastProcessorCountRefreshTicks >= 30000)
        {
          PlatformHelper.s_processorCount = processorCount = Environment.ProcessorCount;
          PlatformHelper.s_lastProcessorCountRefreshTicks = tickCount;
        }
        return processorCount;
      }
    }

    internal static bool IsSingleProcessor
    {
      get
      {
        return PlatformHelper.ProcessorCount == 1;
      }
    }
  }
}
