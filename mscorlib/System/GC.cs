// Decompiled with JetBrains decompiler
// Type: System.GC
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>
  ///   Управляет системным сборщиком мусора — службой, которая автоматически высвобождает неиспользуемую память.
  /// </summary>
  [__DynamicallyInvokable]
  public static class GC
  {
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetGCLatencyMode();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int SetGCLatencyMode(int newLatencyMode);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int _StartNoGCRegion(long totalSize, bool lohSizeKnown, long lohSize, bool disallowFullBlockingGC);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int _EndNoGCRegion();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetLOHCompactionMode();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SetLOHCompactionMode(int newLOHCompactionyMode);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetGenerationWR(IntPtr handle);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern long GetTotalMemory();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _Collect(int generation, int mode);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetMaxGeneration();

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int _CollectionCount(int generation, int getSpecialGCCount);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsServerGC();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _AddMemoryPressure(ulong bytesAllocated);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _RemoveMemoryPressure(ulong bytesAllocated);

    /// <summary>
    ///   Информирует среду выполнения о выделении большого объема неуправляемой памяти, которую необходимо учесть при планировании сборки мусора.
    /// </summary>
    /// <param name="bytesAllocated">
    ///   Дополнительный объем выделенной неуправляемой памяти.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="bytesAllocated" /> меньше или равно 0.
    /// 
    ///   -или-
    /// 
    ///   На 32-разрядном компьютере <paramref name="bytesAllocated" /> больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void AddMemoryPressure(long bytesAllocated)
    {
      if (bytesAllocated <= 0L)
        throw new ArgumentOutOfRangeException(nameof (bytesAllocated), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (4 == IntPtr.Size && bytesAllocated > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException("pressure", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegInt32"));
      GC._AddMemoryPressure((ulong) bytesAllocated);
    }

    /// <summary>
    ///   Информирует среду выполнения о том, что неуправляемая память освобождена и ее более не требуется учитывать при планировании сборки мусора.
    /// </summary>
    /// <param name="bytesAllocated">
    ///   Объем освобожденной неуправляемой памяти.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="bytesAllocated" /> меньше или равно 0.
    /// 
    ///   -или-
    /// 
    ///   На 32-разрядном компьютере <paramref name="bytesAllocated" /> больше, чем <see cref="F:System.Int32.MaxValue" />.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void RemoveMemoryPressure(long bytesAllocated)
    {
      if (bytesAllocated <= 0L)
        throw new ArgumentOutOfRangeException(nameof (bytesAllocated), Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
      if (4 == IntPtr.Size && bytesAllocated > (long) int.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (bytesAllocated), Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegInt32"));
      GC._RemoveMemoryPressure((ulong) bytesAllocated);
    }

    /// <summary>
    ///   Возвращает номер текущего поколения указанного объекта.
    /// </summary>
    /// <param name="obj">
    ///   Объект, информация о поколении которого извлекается.
    /// </param>
    /// <returns>
    ///   Текущий номер поколения <paramref name="obj" />.
    /// </returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetGeneration(object obj);

    /// <summary>
    ///   Принудительно начинает немедленную сборку мусора, начиная с нулевого поколения и вплоть до указанного поколения.
    /// </summary>
    /// <param name="generation">
    ///   Количество старших поколений, для которых следует выполнить сборку мусора.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Недопустимый параметр <paramref name="generation" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static void Collect(int generation)
    {
      GC.Collect(generation, GCCollectionMode.Default);
    }

    /// <summary>
    ///   Принудительно запускает немедленную сборку мусора для всех поколений.
    /// </summary>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Collect()
    {
      GC._Collect(-1, 2);
    }

    /// <summary>
    ///   Принудительно запускает немедленную сборку мусора начиная с нулевого поколения и вплоть до указанного поколения в момент времени, заданный значением <see cref="T:System.GCCollectionMode" />.
    /// </summary>
    /// <param name="generation">
    ///   Количество старших поколений, для которых следует выполнить сборку мусора.
    /// </param>
    /// <param name="mode">
    ///   Значение перечисления, указывающее, является ли сборка мусора принудительной (<see cref="F:System.GCCollectionMode.Default" /> или <see cref="F:System.GCCollectionMode.Forced" />) или оптимизированной (<see cref="F:System.GCCollectionMode.Optimized" />).
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Недопустимый параметр <paramref name="generation" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mode" /> не является одним из значений <see cref="T:System.GCCollectionMode" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Collect(int generation, GCCollectionMode mode)
    {
      GC.Collect(generation, mode, true);
    }

    /// <summary>
    ///   Принудительная сборка мусора с поколения 0 до указанного поколения во время, указанное значением <see cref="T:System.GCCollectionMode" />, со значением, указывающим, должна ли сборка быть блокирующей.
    /// </summary>
    /// <param name="generation">
    ///   Количество старших поколений, для которых следует выполнить сборку мусора.
    /// </param>
    /// <param name="mode">
    ///   Значение перечисления, указывающее, является ли сборка мусора принудительной (<see cref="F:System.GCCollectionMode.Default" /> или <see cref="F:System.GCCollectionMode.Forced" />) или оптимизированной (<see cref="F:System.GCCollectionMode.Optimized" />).
    /// </param>
    /// <param name="blocking">
    ///   Значение <see langword="true" /> для выполнения блокирующей сборки мусора; значение <see langword="false" /> для выполнения фоновой сборки мусора, где это возможно.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Недопустимый параметр <paramref name="generation" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="mode" /> не является одним из значений <see cref="T:System.GCCollectionMode" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Collect(int generation, GCCollectionMode mode, bool blocking)
    {
      GC.Collect(generation, mode, blocking, false);
    }

    /// <summary>
    ///   Принудительная сборка мусора с поколения 0 до указанного поколения во время, указанное значением <see cref="T:System.GCCollectionMode" />, со значениями, указывающими, должна ли сборка быть блокирующей и сжимающей.
    /// </summary>
    /// <param name="generation">
    ///   Количество старших поколений, для которых следует выполнить сборку мусора.
    /// </param>
    /// <param name="mode">
    ///   Значение перечисления, указывающее, является ли сборка мусора принудительной (<see cref="F:System.GCCollectionMode.Default" /> или <see cref="F:System.GCCollectionMode.Forced" />) или оптимизированной (<see cref="F:System.GCCollectionMode.Optimized" />).
    /// </param>
    /// <param name="blocking">
    ///   Значение <see langword="true" /> для выполнения блокирующей сборки мусора; значение <see langword="false" /> для выполнения фоновой сборки мусора, где это возможно.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    /// <param name="compacting">
    ///   Значение <see langword="true" />, чтобы сжимать кучу маленьких объектов; значение <see langword="false" />, чтобы только очищать.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    [SecuritySafeCritical]
    public static void Collect(int generation, GCCollectionMode mode, bool blocking, bool compacting)
    {
      if (generation < 0)
        throw new ArgumentOutOfRangeException(nameof (generation), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      switch (mode)
      {
        case GCCollectionMode.Default:
        case GCCollectionMode.Forced:
        case GCCollectionMode.Optimized:
          int mode1 = 0;
          if (mode == GCCollectionMode.Optimized)
            mode1 |= 4;
          if (compacting)
            mode1 |= 8;
          if (blocking)
            mode1 |= 2;
          else if (!compacting)
            mode1 |= 1;
          GC._Collect(generation, mode1);
          break;
        default:
          throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
      }
    }

    /// <summary>
    ///   Возвращает количество операций сборки мусора, выполненных для заданного поколения объектов.
    /// </summary>
    /// <param name="generation">
    ///   Поколение объектов, для которого будет определено количество операций сборки мусора.
    /// </param>
    /// <returns>
    ///   Количество операций сборки мусора, выполненных для заданного поколения объектов с начала процесса.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="generation" /> меньше 0.
    /// </exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int CollectionCount(int generation)
    {
      if (generation < 0)
        throw new ArgumentOutOfRangeException(nameof (generation), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      return GC._CollectionCount(generation, 0);
    }

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static int CollectionCount(int generation, bool getSpecialGCCount)
    {
      if (generation < 0)
        throw new ArgumentOutOfRangeException(nameof (generation), Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      return GC._CollectionCount(generation, getSpecialGCCount ? 1 : 0);
    }

    /// <summary>
    ///   Ссылается на указанный объект, делая его недоступным для сборщика мусора с момента начала текущей процедуры до вызова этого метода.
    /// </summary>
    /// <param name="obj">Объект для ссылки.</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void KeepAlive(object obj)
    {
    }

    /// <summary>
    ///   Возвращает текущий номер поколения для целевого объекта указанной слабой ссылки.
    /// </summary>
    /// <param name="wo">
    ///   Объект <see cref="T:System.WeakReference" />, указывающий на целевой объект, номер поколения которого требуется определить.
    /// </param>
    /// <returns>
    ///   Текущий номер поколения для целевого объекта <paramref name="wo" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Сборка мусора уже выполнена для <paramref name="wo" />.
    /// </exception>
    [SecuritySafeCritical]
    public static int GetGeneration(WeakReference wo)
    {
      int generationWr = GC.GetGenerationWR(wo.m_handle);
      GC.KeepAlive((object) wo);
      return generationWr;
    }

    /// <summary>
    ///   Возвращает наибольшее число поколений, поддерживаемое системой в настоящее время.
    /// </summary>
    /// <returns>
    ///   Значение от нуля до максимального числа поддерживаемых поколений.
    /// </returns>
    [__DynamicallyInvokable]
    public static int MaxGeneration
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        return GC.GetMaxGeneration();
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void _WaitForPendingFinalizers();

    /// <summary>
    ///   Приостанавливает текущий поток до тех пор, пока поток, обрабатывающий очередь методов завершения, не обработает всю очередь.
    /// </summary>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void WaitForPendingFinalizers()
    {
      GC._WaitForPendingFinalizers();
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _SuppressFinalize(object o);

    /// <summary>
    ///   Сообщает среде CLR, что она на не должна вызывать метод завершения для указанного объекта.
    /// </summary>
    /// <param name="obj">
    ///   Объект, метод завершения для которого не должен выполняться.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void SuppressFinalize(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      GC._SuppressFinalize(obj);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _ReRegisterForFinalize(object o);

    /// <summary>
    ///   Требует, чтобы система вызвала метод завершения для указанного объекта, для которого ранее был вызван метод <see cref="M:System.GC.SuppressFinalize(System.Object)" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект, для которого должен быть вызван метод завершения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void ReRegisterForFinalize(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      GC._ReRegisterForFinalize(obj);
    }

    /// <summary>
    ///   Извлекает предполагаемое количество выделенных в данный момент байтов.
    ///    Параметр указывает, может ли этот метод выдержать короткий интервал времени ожидания перед возвратом, пока система выполняет сборку мусора и завершает объекты.
    /// </summary>
    /// <param name="forceFullCollection">
    ///   Значение <see langword="true" />, указывающий, что перед возвратом этот метод может дождаться выполнения сборки мусора; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Наилучшая доступная аппроксимация числа байтов, распределенных в данный момент в управляемой памяти.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static long GetTotalMemory(bool forceFullCollection)
    {
      long totalMemory = GC.GetTotalMemory();
      if (!forceFullCollection)
        return totalMemory;
      int num1 = 20;
      long num2 = totalMemory;
      float num3;
      do
      {
        GC.WaitForPendingFinalizers();
        GC.Collect();
        long num4 = num2;
        num2 = GC.GetTotalMemory();
        num3 = (float) (num2 - num4) / (float) num4;
      }
      while (num1-- > 0 && (-0.05 >= (double) num3 || (double) num3 >= 0.05));
      return num2;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool _RegisterForFullGCNotification(int maxGenerationPercentage, int largeObjectHeapPercentage);

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool _CancelFullGCNotification();

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int _WaitForFullGCApproach(int millisecondsTimeout);

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int _WaitForFullGCComplete(int millisecondsTimeout);

    /// <summary>
    ///   Указывает, что необходимо отправлять уведомления о сборке мусора, когда соблюдены условия для полной сборки мусора и когда завершена сборка.
    /// </summary>
    /// <param name="maxGenerationThreshold">
    ///   Число от 1 до 99, указывающее условия создания уведомления в зависимости от объектов, выделенных в поколение 2.
    /// </param>
    /// <param name="largeObjectHeapThreshold">
    ///   Число от 1 до 99, указывающее условия создания уведомления в зависимости от объектов, помещенных в кучу больших объектов.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="maxGenerationThreshold " /> или <paramref name="largeObjectHeapThreshold " /> не входит в диапазон от 1 до 99.
    /// </exception>
    [SecurityCritical]
    public static void RegisterForFullGCNotification(int maxGenerationThreshold, int largeObjectHeapThreshold)
    {
      if (maxGenerationThreshold <= 0 || maxGenerationThreshold >= 100)
        throw new ArgumentOutOfRangeException(nameof (maxGenerationThreshold), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), (object) 1, (object) 99));
      if (largeObjectHeapThreshold <= 0 || largeObjectHeapThreshold >= 100)
        throw new ArgumentOutOfRangeException(nameof (largeObjectHeapThreshold), string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), (object) 1, (object) 99));
      if (!GC._RegisterForFullGCNotification(maxGenerationThreshold, largeObjectHeapThreshold))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotWithConcurrentGC"));
    }

    /// <summary>Отменяет регистрацию уведомления о сборке мусора.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот член недоступен, если включена параллельная сборка мусора.
    ///    В разделе  &lt;gcConcurrent&gt;  Дополнительные сведения об отключении параллельной сборки мусора.&lt;/gcConcurrent&gt;
    /// </exception>
    [SecurityCritical]
    public static void CancelFullGCNotification()
    {
      if (!GC._CancelFullGCNotification())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotWithConcurrentGC"));
    }

    /// <summary>
    ///   Возвращает состояние зарегистрированного уведомления, чтобы определить, является ли неизбежной полная, блокирующая сборка мусора средой CLR.
    /// </summary>
    /// <returns>
    ///   Состояние зарегистрированного уведомления о сборке мусора.
    /// </returns>
    [SecurityCritical]
    public static GCNotificationStatus WaitForFullGCApproach()
    {
      return (GCNotificationStatus) GC._WaitForFullGCApproach(-1);
    }

    /// <summary>
    ///   Возвращает состояние зарегистрированного уведомления в пределах указанного времени ожидания, чтобы определить, является ли неизбежной полная блокировка сборки мусора средой CLR.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Продолжительность времени ожидания, прежде чем можно извлечь состояние уведомления.
    ///    Укажите значение -1, если период ожидания неограниченный.
    /// </param>
    /// <returns>
    ///   Состояние зарегистрированного уведомления о сборке мусора.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="millisecondsTimeout" /> должно быть либо неотрицательным, либо меньше или равно <see cref="F:System.Int32.MaxValue" /> или –1.
    /// </exception>
    [SecurityCritical]
    public static GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return (GCNotificationStatus) GC._WaitForFullGCApproach(millisecondsTimeout);
    }

    /// <summary>
    ///   Возвращает состояние зарегистрированного уведомления, чтобы определить, завершена ли полная блокировка сборки мусора средой CLR.
    /// </summary>
    /// <returns>
    ///   Состояние зарегистрированного уведомления о сборке мусора.
    /// </returns>
    [SecurityCritical]
    public static GCNotificationStatus WaitForFullGCComplete()
    {
      return (GCNotificationStatus) GC._WaitForFullGCComplete(-1);
    }

    /// <summary>
    ///   Возвращает состояние зарегистрированного уведомления в пределах указанного времени ожидания, чтобы определить, завершена ли полная блокировка сборки мусора средой CLR.
    /// </summary>
    /// <param name="millisecondsTimeout">
    ///   Продолжительность времени ожидания, прежде чем можно извлечь состояние уведомления.
    ///    Укажите значение -1, если период ожидания неограниченный.
    /// </param>
    /// <returns>
    ///   Состояние зарегистрированного уведомления о сборке мусора.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Значение <paramref name="millisecondsTimeout" /> должно быть либо неотрицательным, либо меньше или равно <see cref="F:System.Int32.MaxValue" /> или –1.
    /// </exception>
    [SecurityCritical]
    public static GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout)
    {
      if (millisecondsTimeout < -1)
        throw new ArgumentOutOfRangeException(nameof (millisecondsTimeout), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
      return (GCNotificationStatus) GC._WaitForFullGCComplete(millisecondsTimeout);
    }

    [SecurityCritical]
    private static bool StartNoGCRegionWorker(long totalSize, bool hasLohSize, long lohSize, bool disallowFullBlockingGC)
    {
      switch ((GC.StartNoGCRegionStatus) GC._StartNoGCRegion(totalSize, hasLohSize, lohSize, disallowFullBlockingGC))
      {
        case GC.StartNoGCRegionStatus.NotEnoughMemory:
          return false;
        case GC.StartNoGCRegionStatus.AmountTooLarge:
          throw new ArgumentOutOfRangeException(nameof (totalSize), "totalSize is too large. For more information about setting the maximum size, see \"Latency Modes\" in http://go.microsoft.com/fwlink/?LinkId=522706");
        case GC.StartNoGCRegionStatus.AlreadyInProgress:
          throw new InvalidOperationException("The NoGCRegion mode was already in progress");
        default:
          return true;
      }
    }

    /// <summary>
    ///   Пытается запретить сборку мусора во время выполнения критического пути, если доступен указанный достаточный объем памяти.
    /// </summary>
    /// <param name="totalSize">
    ///   Объем памяти в байтах для выделения без запуска сборки мусора.
    ///    Он должен быть меньше или равен размеру временного сегмента.
    ///    Сведения о размере эфемерных сегментов см. в разделе "Эфемерные поколения и сегменты" статьи Fundamentals of Garbage Collection.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если среде выполнения удалось зафиксировать необходимый объем памяти и сборщик мусора может перейти в режим задержки без области сборки мусора; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="totalSize" /> превышает размер эфемерного сегмента.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Процесс уже находится в режиме задержки без области сборки мусора.
    /// </exception>
    [SecurityCritical]
    public static bool TryStartNoGCRegion(long totalSize)
    {
      return GC.StartNoGCRegionWorker(totalSize, false, 0L, false);
    }

    /// <summary>
    ///   Пытается запретить сборку мусора во время выполнения критического пути, если указанный объем памяти доступен для кучи больших объектов и для кучи маленьких объектов.
    /// </summary>
    /// <param name="totalSize">
    ///   Объем памяти в байтах для выделения без запуска сборки мусора.
    ///    Значение <paramref name="totalSize" /> – <paramref name="lohSize" /> должно быть меньше или равно размеру временного сегмента.
    ///    Сведения о размере эфемерных сегментов см. в разделе "Эфемерные поколения и сегменты" статьи Fundamentals of Garbage Collection.
    /// </param>
    /// <param name="lohSize">
    ///   Число байтов в <paramref name="totalSize" /> для назначения кучи больших объектов.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если среде выполнения удалось зафиксировать необходимый объем памяти и сборщик мусора может перейти в режим задержки без области сборки мусора; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="totalSize" /> — <paramref name="lohSize" /> превышает размер эфемерного сегмента.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Процесс уже находится в режиме задержки без области сборки мусора.
    /// </exception>
    [SecurityCritical]
    public static bool TryStartNoGCRegion(long totalSize, long lohSize)
    {
      return GC.StartNoGCRegionWorker(totalSize, true, lohSize, false);
    }

    /// <summary>
    ///   Пытается запретить сборку мусора во время выполнения критического пути, если доступен указанный объем памяти, и устанавливает, будет ли выполняться полная блокирующая сборка мусора, если изначально не хватает памяти.
    /// </summary>
    /// <param name="totalSize">
    ///   Объем памяти в байтах для выделения без запуска сборки мусора.
    ///    Он должен быть меньше или равен размеру временного сегмента.
    ///    Сведения о размере эфемерных сегментов см. в разделе "Эфемерные поколения и сегменты" статьи Fundamentals of Garbage Collection.
    /// </param>
    /// <param name="disallowFullBlockingGC">
    ///   Значение <see langword="true" />, чтобы пропустить полную блокирующую сборку мусора, если сборщику мусора изначально не удалось выделить <paramref name="totalSize" /> байтов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если среде выполнения удалось зафиксировать необходимый объем памяти и сборщик мусора может перейти в режим задержки без области сборки мусора; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="totalSize" /> превышает размер эфемерного сегмента.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Процесс уже находится в режиме задержки без области сборки мусора.
    /// </exception>
    [SecurityCritical]
    public static bool TryStartNoGCRegion(long totalSize, bool disallowFullBlockingGC)
    {
      return GC.StartNoGCRegionWorker(totalSize, false, 0L, disallowFullBlockingGC);
    }

    /// <summary>
    ///   Пытается запретить сборку мусора во время выполнения критического пути, если доступен указанный объем памяти для кучи больших объектов и для кучи маленьких объектов, и устанавливает, будет ли выполняться полная блокирующая сборка мусора, если изначально не хватает памяти.
    /// </summary>
    /// <param name="totalSize">
    ///   Объем памяти в байтах для выделения без запуска сборки мусора.
    ///    Значение <paramref name="totalSize" /> – <paramref name="lohSize" /> должно быть меньше или равно размеру временного сегмента.
    ///    Сведения о размере эфемерных сегментов см. в разделе "Эфемерные поколения и сегменты" статьи Fundamentals of Garbage Collection.
    /// </param>
    /// <param name="lohSize">
    ///   Число байтов в <paramref name="totalSize" /> для назначения кучи больших объектов.
    /// </param>
    /// <param name="disallowFullBlockingGC">
    ///   Значение <see langword="true" />, чтобы пропустить полную блокирующую сборку мусора, если сборщику мусора изначально не удалось выделить указанную память в куче малых и в куче больших объектов; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если среде выполнения удалось зафиксировать необходимый объем памяти и сборщик мусора может перейти в режим задержки без области сборки мусора; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="totalSize" /> — <paramref name="lohSize" /> превышает размер эфемерного сегмента.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Процесс уже находится в режиме задержки без области сборки мусора.
    /// </exception>
    [SecurityCritical]
    public static bool TryStartNoGCRegion(long totalSize, long lohSize, bool disallowFullBlockingGC)
    {
      return GC.StartNoGCRegionWorker(totalSize, true, lohSize, disallowFullBlockingGC);
    }

    [SecurityCritical]
    private static GC.EndNoGCRegionStatus EndNoGCRegionWorker()
    {
      switch ((GC.EndNoGCRegionStatus) GC._EndNoGCRegion())
      {
        case GC.EndNoGCRegionStatus.NotInProgress:
          throw new InvalidOperationException("NoGCRegion mode must be set");
        case GC.EndNoGCRegionStatus.GCInduced:
          throw new InvalidOperationException("Garbage collection was induced in NoGCRegion mode");
        case GC.EndNoGCRegionStatus.AllocationExceeded:
          throw new InvalidOperationException("Allocated memory exceeds specified memory for NoGCRegion mode");
        default:
          return GC.EndNoGCRegionStatus.Succeeded;
      }
    }

    /// <summary>Завершает режим задержки без области сборки мусора.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Сборщик мусора не находится в режиме задержки без области сборки мусора.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// 
    ///   -или-
    /// 
    ///   Режим задержки без области сборки мусора был завершен заранее, поскольку была вызвана сборка мусора.
    /// 
    ///   -или-
    /// 
    ///   Выделение памяти превышает объем, указанный в вызове метода <see cref="M:System.GC.TryStartNoGCRegion(System.Int64)" />.
    /// </exception>
    [SecurityCritical]
    public static void EndNoGCRegion()
    {
      int num = (int) GC.EndNoGCRegionWorker();
    }

    private enum StartNoGCRegionStatus
    {
      Succeeded,
      NotEnoughMemory,
      AmountTooLarge,
      AlreadyInProgress,
    }

    private enum EndNoGCRegionStatus
    {
      Succeeded,
      NotInProgress,
      GCInduced,
      AllocationExceeded,
    }
  }
}
