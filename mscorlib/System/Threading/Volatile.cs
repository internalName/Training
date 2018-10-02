// Decompiled with JetBrains decompiler
// Type: System.Threading.Volatile
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Содержит методы для выполнения операций энергонезависимой памяти.
  /// </summary>
  [__DynamicallyInvokable]
  public static class Volatile
  {
    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static bool Read(ref bool location)
    {
      bool flag = location;
      Thread.MemoryBarrier();
      return flag;
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte Read(ref sbyte location)
    {
      sbyte num = location;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static byte Read(ref byte location)
    {
      byte num = location;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static short Read(ref short location)
    {
      short num = location;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static ushort Read(ref ushort location)
    {
      ushort num = location;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int Read(ref int location)
    {
      int num = location;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static uint Read(ref uint location)
    {
      uint num = location;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static long Read(ref long location)
    {
      return Interlocked.CompareExchange(ref location, 0L, 0L);
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe ulong Read(ref ulong location)
    {
      fixed (ulong* numPtr = &location)
      {
        // ISSUE: cast to a reference type
        return (ulong) Interlocked.CompareExchange((long&) numPtr, 0L, 0L);
      }
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static IntPtr Read(ref IntPtr location)
    {
      IntPtr num = location;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    public static UIntPtr Read(ref UIntPtr location)
    {
      UIntPtr num = location;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static float Read(ref float location)
    {
      float num = location;
      Thread.MemoryBarrier();
      return num;
    }

    /// <summary>
    ///   Считывает значение указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <returns>
    ///   Прочитанное значение.
    ///    Это значение является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static double Read(ref double location)
    {
      return Interlocked.CompareExchange(ref location, 0.0, 0.0);
    }

    /// <summary>
    ///   Ссылка на объект считывает из указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись после этого метода в коде, обработчик будет невозможно переместить перед вызовом этого метода.
    /// </summary>
    /// <param name="location">Поле для чтения.</param>
    /// <typeparam name="T">
    ///   Тип поля для чтения.
    ///    Это должен быть ссылочным типом, а не тип значения.
    /// </typeparam>
    /// <returns>
    ///   Ссылка на <paramref name="T" /> было прочитано.
    ///    Эта ссылка является последним записанным каким-либо из процессоров компьютера, независимо от количества процессоров или состояние кэша процессора.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static T Read<T>(ref T location) where T : class
    {
      T obj = location;
      Thread.MemoryBarrier();
      return obj;
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref bool location, bool value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static void Write(ref sbyte location, sbyte value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref byte location, byte value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref short location, short value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static void Write(ref ushort location, ushort value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref int location, int value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static void Write(ref uint location, uint value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если операцию памяти перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref long location, long value)
    {
      Interlocked.Exchange(ref location, value);
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static unsafe void Write(ref ulong location, ulong value)
    {
      fixed (ulong* numPtr = &location)
      {
        // ISSUE: cast to a reference type
        Interlocked.Exchange((long&) numPtr, (long) value);
      }
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static void Write(ref IntPtr location, IntPtr value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [CLSCompliant(false)]
    public static void Write(ref UIntPtr location, UIntPtr value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref float location, float value)
    {
      Thread.MemoryBarrier();
      location = value;
    }

    /// <summary>
    ///   Записывает заданное значение для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться значение.
    /// </param>
    /// <param name="value">
    ///   Значение для записи.
    ///    Значение записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static void Write(ref double location, double value)
    {
      Interlocked.Exchange(ref location, value);
    }

    /// <summary>
    ///   Записывает ссылку на указанный объект для указанного поля.
    ///    В системах, которые их используют, вставляет барьера памяти, которая предотвращает изменение порядка операций памяти следующим процессор: Если чтение или запись отображается перед вызовом этого метода в коде, обработчик будет невозможно переместить после этого метода.
    /// </summary>
    /// <param name="location">
    ///   Поле, куда будут записываться ссылка на объект.
    /// </param>
    /// <param name="value">
    ///   Ссылка на объект для записи.
    ///    Ссылка записывается непосредственно, чтобы снова становится видимым для всех процессоров компьютера.
    /// </param>
    /// <typeparam name="T">
    ///   Тип поля для записи.
    ///    Это должен быть ссылочным типом, а не тип значения.
    /// </typeparam>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Write<T>(ref T location, T value) where T : class
    {
      Thread.MemoryBarrier();
      location = value;
    }
  }
}
