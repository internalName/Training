// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.RuntimeHelpers
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Предоставляет набор статических методов и свойств, которые обеспечивают поддержку компиляторов.
  ///    Этот класс не наследуется.
  /// </summary>
  [__DynamicallyInvokable]
  public static class RuntimeHelpers
  {
    /// <summary>
    ///   Предоставляет быстрый способ инициализации массива данных, хранящихся в модуле.
    /// </summary>
    /// <param name="array">Массив должен быть инициализирован.</param>
    /// <param name="fldHandle">
    ///   Дескриптор поля, указывающий расположение данных, используемых для инициализации массива.
    /// </param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void InitializeArray(Array array, RuntimeFieldHandle fldHandle);

    /// <summary>Тип значения упаковывается.</summary>
    /// <param name="obj">Тип значения должен быть упакован.</param>
    /// <returns>
    ///   Упакованной копии <paramref name="obj" /> Если это класс значения; в противном случае — <paramref name="obj" /> сам.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetObjectValue(object obj);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _RunClassConstructor(RuntimeType type);

    /// <summary>Выполняет указанный метод конструктора класса.</summary>
    /// <param name="type">
    ///   Дескриптор типа, определяющий метод конструктора класса для запуска.
    /// </param>
    /// <exception cref="T:System.TypeInitializationException">
    ///   Инициализатор класса создает исключение.
    /// </exception>
    [__DynamicallyInvokable]
    public static void RunClassConstructor(RuntimeTypeHandle type)
    {
      RuntimeHelpers._RunClassConstructor(type.GetRuntimeType());
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _RunModuleConstructor(RuntimeModule module);

    /// <summary>Выполняет указанный метод конструктора модуля.</summary>
    /// <param name="module">
    ///   Дескриптор, определяющий метод конструктора модуля для запуска.
    /// </param>
    /// <exception cref="T:System.TypeInitializationException">
    ///   Конструктор модуля выдает исключение.
    /// </exception>
    public static void RunModuleConstructor(ModuleHandle module)
    {
      RuntimeHelpers._RunModuleConstructor(module.GetRuntimeModule());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void _PrepareMethod(IRuntimeMethodInfo method, IntPtr* pInstantiation, int cInstantiation);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void _CompileMethod(IRuntimeMethodInfo method);

    /// <summary>
    ///   Подготавливает метод для включения в области ограниченного выполнения (CER).
    /// </summary>
    /// <param name="method">Дескриптор метода для подготовки.</param>
    [SecurityCritical]
    public static unsafe void PrepareMethod(RuntimeMethodHandle method)
    {
      RuntimeHelpers._PrepareMethod(method.GetMethodInfo(), (IntPtr*) null, 0);
    }

    /// <summary>
    ///   Подготавливает метод для включения в области ограниченного выполнения (CER) с указанной реализацией.
    /// </summary>
    /// <param name="method">Дескриптор метода для подготовки.</param>
    /// <param name="instantiation">
    ///   Экземпляр для передачи в метод.
    /// </param>
    [SecurityCritical]
    public static unsafe void PrepareMethod(RuntimeMethodHandle method, RuntimeTypeHandle[] instantiation)
    {
      int length;
      fixed (IntPtr* pInstantiation = RuntimeTypeHandle.CopyRuntimeTypeHandles(instantiation, out length))
      {
        RuntimeHelpers._PrepareMethod(method.GetMethodInfo(), pInstantiation, length);
        GC.KeepAlive((object) instantiation);
      }
    }

    /// <summary>
    ///   Указывает, что заданный делегат следует подготовить к включению в области ограниченного выполнения (CER).
    /// </summary>
    /// <param name="d">Тип делегата для подготовки.</param>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void PrepareDelegate(Delegate d);

    /// <summary>
    ///   Предоставляет способ для приложений динамической подготовки <see cref="T:System.AppDomain" /> делегаты событий.
    /// </summary>
    /// <param name="d">Чтобы подготовить делегата события.</param>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void PrepareContractedDelegate(Delegate d);

    /// <summary>
    ///   Служит хэш-функцией для определенного объекта и подходит для использования в алгоритмах и структурах данных, использующих хэш-кодов, таких как хэш-таблицы.
    /// </summary>
    /// <param name="o">Для извлечения хэш-код для объекта.</param>
    /// <returns>
    ///   Хэш-код для объекта, указанного в <paramref name="o" /> параметре.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int GetHashCode(object o);

    /// <summary>
    ///   Определяет, является ли указанный <see cref="T:System.Object" /> считаются равными.
    /// </summary>
    /// <param name="o1">Первый из сравниваемых объектов.</param>
    /// <param name="o2">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="o1" /> параметр является тем же экземпляром, как <paramref name="o2" /> параметр, или оба являются <see langword="null" />, или если o1.Equals(o2) возвращает <see langword="true" />; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public new static extern bool Equals(object o1, object o2);

    /// <summary>
    ///   Возвращает смещение в байтах к данным в указанной строке.
    /// </summary>
    /// <returns>
    ///   Смещение в байтах от начала <see cref="T:System.String" /> объекта для первого символа в строке.
    /// </returns>
    [__DynamicallyInvokable]
    public static int OffsetToStringData
    {
      [NonVersionable, __DynamicallyInvokable] get
      {
        return 8;
      }
    }

    /// <summary>
    ///   Обеспечивает достаточно велик, чтобы выполнить функцию average .NET Framework оставшееся пространство стека.
    /// </summary>
    /// <exception cref="T:System.InsufficientExecutionStackException">
    ///   Доступное пространство стека недостаточно для выполнения среднее функцию платформы .NET Framework.
    /// </exception>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void EnsureSufficientExecutionStack();

    /// <summary>
    ///   Проверяет наличие определенного пространства стека, чтобы убедиться, что переполнение стека не может произойти в последующем блоке кода (предполагая, что код использует только конечный и небольшой объем стекового пространства).
    ///    Мы рекомендуем использовать области ограниченного выполнения (CER), вместо этого метода.
    /// </summary>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void ProbeForSufficientStack();

    /// <summary>
    ///   Назначает основную часть кода области ограниченного выполнения (CER).
    /// </summary>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static void PrepareConstrainedRegions()
    {
      RuntimeHelpers.ProbeForSufficientStack();
    }

    /// <summary>
    ///   Назначает основную часть кода области ограниченного выполнения (CER) без какой-либо проверки.
    /// </summary>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    public static void PrepareConstrainedRegionsNoOP()
    {
    }

    /// <summary>
    ///   Выполняет код, используя <see cref="T:System.Delegate" /> при использовании другой <see cref="T:System.Delegate" /> для выполнения дополнительного кода в случае исключения.
    /// </summary>
    /// <param name="code">Делегат для кода попробовать.</param>
    /// <param name="backoutCode">
    ///   Делегат для кода для выполнения при возникновении исключения.
    /// </param>
    /// <param name="userData">
    ///   Передать данные <paramref name="code" /> и <paramref name="backoutCode" />.
    /// </param>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void ExecuteCodeWithGuaranteedCleanup(RuntimeHelpers.TryCode code, RuntimeHelpers.CleanupCode backoutCode, object userData);

    [PrePrepareMethod]
    internal static void ExecuteBackoutCodeHelper(object backoutCode, object userData, bool exceptionThrown)
    {
      ((RuntimeHelpers.CleanupCode) backoutCode)(userData, exceptionThrown);
    }

    /// <summary>
    ///   Представляет делегат для кода, который должен выполняться в блоке try...
    /// </summary>
    /// <param name="userData">Данные, передаваемые делегату.</param>
    public delegate void TryCode(object userData);

    /// <summary>
    ///   Представляет метод, выполняемый при возникновении исключения.
    /// </summary>
    /// <param name="userData">Данные, передаваемые делегату.</param>
    /// <param name="exceptionThrown">
    ///   <see langword="true" /> для выражения, которое произошло исключение; в противном случае — <see langword="false" />.
    /// </param>
    public delegate void CleanupCode(object userData, bool exceptionThrown);
  }
}
