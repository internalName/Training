// Decompiled with JetBrains decompiler
// Type: System.Runtime.GCSettings
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime
{
  /// <summary>
  ///   Указывает параметры сборки мусора для текущего процесса.
  /// </summary>
  [__DynamicallyInvokable]
  public static class GCSettings
  {
    /// <summary>
    ///   Возвращает или задает текущий режим задержки для сборки мусора.
    /// </summary>
    /// <returns>
    ///   Одно из значений из перечисления, задающее режим задержки.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Для свойства <see cref="P:System.Runtime.GCSettings.LatencyMode" /> задано недопустимое значение.
    /// 
    ///   -или-
    /// 
    ///   Нельзя задать для свойства <see cref="P:System.Runtime.GCSettings.LatencyMode" /> значение <see cref="F:System.Runtime.GCLatencyMode.NoGCRegion" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static GCLatencyMode LatencyMode
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        return (GCLatencyMode) GC.GetGCLatencyMode();
      }
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable, HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)] set
      {
        switch (value)
        {
          case GCLatencyMode.Batch:
          case GCLatencyMode.Interactive:
          case GCLatencyMode.LowLatency:
          case GCLatencyMode.SustainedLowLatency:
            if (GC.SetGCLatencyMode((int) value) != 1)
              break;
            throw new InvalidOperationException("The NoGCRegion mode is in progress. End it and then set a different mode.");
          default:
            throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        }
      }
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Возвращает или задает значение, которое указывает, будет ли куча больших объектов (LOH) сжата во время полной блокирующей сборки мусора.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, указывающее, сжимается ли LOH при полной блокирующей сборке мусора.
    /// </returns>
    [__DynamicallyInvokable]
    public static GCLargeObjectHeapCompactionMode LargeObjectHeapCompactionMode
    {
      [SecuritySafeCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        return (GCLargeObjectHeapCompactionMode) GC.GetLOHCompactionMode();
      }
      [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable, HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)] set
      {
        if (value < GCLargeObjectHeapCompactionMode.Default || value > GCLargeObjectHeapCompactionMode.CompactOnce)
          throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
        GC.SetLOHCompactionMode((int) value);
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, включена ли сборка мусора на сервере.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если сборка мусора на сервере включена; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsServerGC
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return GC.IsServerGC();
      }
    }

    private enum SetLatencyModeStatus
    {
      Succeeded,
      NoGCInProgress,
    }
  }
}
