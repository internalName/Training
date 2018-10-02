// Decompiled with JetBrains decompiler
// Type: System.Threading.Interlocked
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет атомарные операции для переменных, общедоступных нескольким потокам.
  /// </summary>
  [__DynamicallyInvokable]
  public static class Interlocked
  {
    /// <summary>
    ///   Увеличивает значение заданной переменной и сохраняет результат в виде атомарной операции.
    /// </summary>
    /// <param name="location">
    ///   Переменная, значение которой должно увеличиваться.
    /// </param>
    /// <returns>Увеличиваемое значение.</returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int Increment(ref int location)
    {
      return Interlocked.Add(ref location, 1);
    }

    /// <summary>
    ///   Увеличивает значение заданной переменной и сохраняет результат в виде атомарной операции.
    /// </summary>
    /// <param name="location">
    ///   Переменная, значение которой должно увеличиваться.
    /// </param>
    /// <returns>Увеличиваемое значение.</returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static long Increment(ref long location)
    {
      return Interlocked.Add(ref location, 1L);
    }

    /// <summary>
    ///   Уменьшает значение заданной переменной и сохраняет результат в виде атомарной операции.
    /// </summary>
    /// <param name="location">
    ///   Переменная, значение которой уменьшается.
    /// </param>
    /// <returns>Уменьшаемое значение.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Адрес <paramref name="location" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int Decrement(ref int location)
    {
      return Interlocked.Add(ref location, -1);
    }

    /// <summary>
    ///   Уменьшает значение заданной переменной и сохраняет результат в виде атомарной операции.
    /// </summary>
    /// <param name="location">
    ///   Переменная, значение которой уменьшается.
    /// </param>
    /// <returns>Уменьшаемое значение.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Адрес <paramref name="location" /> является пустым указателем.
    /// </exception>
    [__DynamicallyInvokable]
    public static long Decrement(ref long location)
    {
      return Interlocked.Add(ref location, -1L);
    }

    /// <summary>
    ///   Присваивает 32-разрядному целому числу со знаком заданное значение и возвращает исходное значение в виде атомарной операции.
    /// </summary>
    /// <param name="location1">
    ///   Переменная, которая задается указанным значением.
    /// </param>
    /// <param name="value">
    ///   Значение, которое задается для параметра <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int Exchange(ref int location1, int value);

    /// <summary>
    ///   Присваивает 64-разрядному целому числу со знаком заданное значение и возвращает исходное значение в виде атомарной операции.
    /// </summary>
    /// <param name="location1">
    ///   Переменная, которая задается указанным значением.
    /// </param>
    /// <param name="value">
    ///   Значение, которое задается для параметра <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern long Exchange(ref long location1, long value);

    /// <summary>
    ///   Задает число с плавающей запятой с одинарной точностью указанным значением в виде атомарной операции и возвращает исходное значение.
    /// </summary>
    /// <param name="location1">
    ///   Переменная, которая задается указанным значением.
    /// </param>
    /// <param name="value">
    ///   Значение, которое задается для параметра <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern float Exchange(ref float location1, float value);

    /// <summary>
    ///   Задает число с плавающей запятой с двойной точностью указанным значением в виде атомарной операции и возвращает исходное значение.
    /// </summary>
    /// <param name="location1">
    ///   Переменная, которая задается указанным значением.
    /// </param>
    /// <param name="value">
    ///   Значение, которое задается для параметра <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Exchange(ref double location1, double value);

    /// <summary>
    ///   Задает объект указанным значением в виде атомарной операции и возвращает ссылку на исходный объект.
    /// </summary>
    /// <param name="location1">
    ///   Переменная, которая задается указанным значением.
    /// </param>
    /// <param name="value">
    ///   Значение, которое задается для параметра <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object Exchange(ref object location1, object value);

    /// <summary>
    ///   Задает указатель или обработчик, зависящий от платформы в виде атомарной операции, и возвращает ссылку на исходное значение.
    /// </summary>
    /// <param name="location1">
    ///   Переменная, которая задается указанным значением.
    /// </param>
    /// <param name="value">
    ///   Значение, которое задается для параметра <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr Exchange(ref IntPtr location1, IntPtr value);

    /// <summary>
    ///   Задает определенное значение для переменной указанного типа <paramref name="T" /> и возвращает исходное значение (атомарная операция).
    /// </summary>
    /// <param name="location1">
    ///   Переменная, которая задается указанным значением.
    ///    Это ссылочный параметр (<see langword="ref" /> в C#, <see langword="ByRef" /> в Visual Basic).
    /// </param>
    /// <param name="value">
    ///   Значение, которое задается для параметра <paramref name="location1" />.
    /// </param>
    /// <typeparam name="T">
    ///   Тип, который должен использоваться для <paramref name="location1" /> и <paramref name="value" />.
    ///    Этот тип должен быть ссылочным типом.
    /// </typeparam>
    /// <returns>
    ///   Исходное значение <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [ComVisible(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static T Exchange<T>(ref T location1, T value) where T : class
    {
      Interlocked._Exchange(__makeref (location1), __makeref (value));
      return value;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _Exchange(TypedReference location1, TypedReference value);

    /// <summary>
    ///   Сравнивает два 32-разрядных целых числа со знаком на равенство и, если они равны, заменяет первое.
    /// </summary>
    /// <param name="location1">
    ///   Целевой объект, который нужно сравнить с объектом <paramref name="comparand" /> и, возможно, заменить.
    /// </param>
    /// <param name="value">
    ///   Значение, которым будет заменено целевое значение, если проверка покажет равенство.
    /// </param>
    /// <param name="comparand">
    ///   Значение, которое сравнивается со значением в позиции <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение в позиции <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern int CompareExchange(ref int location1, int value, int comparand);

    /// <summary>
    ///   Сравнивает два 64-разрядных целых числа со знаком на равенство и, если они равны, заменяет первое.
    /// </summary>
    /// <param name="location1">
    ///   Целевой объект, который нужно сравнить с объектом <paramref name="comparand" /> и, возможно, заменить.
    /// </param>
    /// <param name="value">
    ///   Значение, которым будет заменено целевое значение, если проверка покажет равенство.
    /// </param>
    /// <param name="comparand">
    ///   Значение, которое сравнивается со значением в позиции <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение в позиции <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern long CompareExchange(ref long location1, long value, long comparand);

    /// <summary>
    ///   Сравнивает два числа с плавающей запятой с обычной точностью на равенство и, если они равны, заменяет первое значение.
    /// </summary>
    /// <param name="location1">
    ///   Целевой объект, который нужно сравнить с объектом <paramref name="comparand" /> и, возможно, заменить.
    /// </param>
    /// <param name="value">
    ///   Значение, которым будет заменено целевое значение, если проверка покажет равенство.
    /// </param>
    /// <param name="comparand">
    ///   Значение, которое сравнивается со значением в позиции <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение в позиции <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern float CompareExchange(ref float location1, float value, float comparand);

    /// <summary>
    ///   Сравнивает два числа с плавающей запятой двойной точности на равенство и, если они равны, заменяет первое значение.
    /// </summary>
    /// <param name="location1">
    ///   Целевой объект, который нужно сравнить с объектом <paramref name="comparand" /> и, возможно, заменить.
    /// </param>
    /// <param name="value">
    ///   Значение, которым будет заменено целевое значение, если проверка покажет равенство.
    /// </param>
    /// <param name="comparand">
    ///   Значение, которое сравнивается со значением в позиции <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение в позиции <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double CompareExchange(ref double location1, double value, double comparand);

    /// <summary>
    ///   Сравнивает два объекта на равенство ссылок и, если они равны, заменяет первый объект.
    /// </summary>
    /// <param name="location1">
    ///   Целевой объект, который сравнивается с <paramref name="comparand" /> и, возможно, будет заменено.
    /// </param>
    /// <param name="value">
    ///   Объект, который заменит целевой объект, если результатом сравнения будет равенство.
    /// </param>
    /// <param name="comparand">
    ///   Объект, который сравнивается с объектом в <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение в позиции <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object CompareExchange(ref object location1, object value, object comparand);

    /// <summary>
    ///   Сравнивает два зависящих от платформы обработчика или указателя на равенство и, если они равны, заменяет первое из значений.
    /// </summary>
    /// <param name="location1">
    ///   Назначение <see cref="T:System.IntPtr" />, значение которого сравнивается со значением <paramref name="comparand" /> и возможно заменяется <paramref name="value" />.
    /// </param>
    /// <param name="value">
    ///   <see cref="T:System.IntPtr" /> Заменит целевое значение, если результатом сравнения будет равенство.
    /// </param>
    /// <param name="comparand">
    ///   <see cref="T:System.IntPtr" /> Сравнивается со значением <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Исходное значение в позиции <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern IntPtr CompareExchange(ref IntPtr location1, IntPtr value, IntPtr comparand);

    /// <summary>
    ///   Сравнивает два экземпляра указанного ссылочного типа <paramref name="T" /> на равенство и, если они равны, заменяет первый из них.
    /// </summary>
    /// <param name="location1">
    ///   Целевой объект, который нужно сравнить с объектом <paramref name="comparand" /> и, возможно, заменить.
    ///    Это ссылочный параметр (<see langword="ref" /> в C#, <see langword="ByRef" /> в Visual Basic).
    /// </param>
    /// <param name="value">
    ///   Значение, которым будет заменено целевое значение, если проверка покажет равенство.
    /// </param>
    /// <param name="comparand">
    ///   Значение, которое сравнивается со значением в позиции <paramref name="location1" />.
    /// </param>
    /// <typeparam name="T">
    ///   Тип, используемый для <paramref name="location1" />, <paramref name="value" />, и <paramref name="comparand" />.
    ///    Этот тип должен быть ссылочным типом.
    /// </typeparam>
    /// <returns>
    ///   Исходное значение в позиции <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [ComVisible(false)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static T CompareExchange<T>(ref T location1, T value, T comparand) where T : class
    {
      Interlocked._CompareExchange(__makeref (location1), __makeref (value), (object) comparand);
      return value;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _CompareExchange(TypedReference location1, TypedReference value, object comparand);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int CompareExchange(ref int location1, int value, int comparand, ref bool succeeded);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int ExchangeAdd(ref int location1, int value);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern long ExchangeAdd(ref long location1, long value);

    /// <summary>
    ///   Добавляет два 32-разрядных целых числа и заменяет первое число на сумму в виде атомарной операции.
    /// </summary>
    /// <param name="location1">
    ///   Переменная, содержащая первое добавляемое значение.
    ///    Сумма двух значений сохраняется в <paramref name="location1" />.
    /// </param>
    /// <param name="value">
    ///   Значение, добавляемое к целому в <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Новое значение сохраняется в <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static int Add(ref int location1, int value)
    {
      return Interlocked.ExchangeAdd(ref location1, value) + value;
    }

    /// <summary>
    ///   Добавляет два 64-разрядных целых числа и заменяет первое число на сумму в виде атомарной операции.
    /// </summary>
    /// <param name="location1">
    ///   Переменная, содержащая первое добавляемое значение.
    ///    Сумма двух значений сохраняется в <paramref name="location1" />.
    /// </param>
    /// <param name="value">
    ///   Значение, добавляемое к целому в <paramref name="location1" />.
    /// </param>
    /// <returns>
    ///   Новое значение сохраняется в <paramref name="location1" />.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    ///   Адрес <paramref name="location1" /> является пустым указателем.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static long Add(ref long location1, long value)
    {
      return Interlocked.ExchangeAdd(ref location1, value) + value;
    }

    /// <summary>
    ///   Возвращает 64-разрядное значение, загруженное в виде атомарной операции.
    /// </summary>
    /// <param name="location">Загружаемое 64-разрядное значение.</param>
    /// <returns>Загруженное значение.</returns>
    [__DynamicallyInvokable]
    public static long Read(ref long location)
    {
      return Interlocked.CompareExchange(ref location, 0L, 0L);
    }

    /// <summary>
    ///   Синхронизирует доступ к памяти следующим образом: процессор, выполняющий текущий поток не способен упорядочить инструкции так, что обращения к памяти до вызова <see cref="M:System.Threading.Interlocked.MemoryBarrier" /> выполнялись после обращений к памяти, выполните вызов <see cref="M:System.Threading.Interlocked.MemoryBarrier" />.
    /// </summary>
    [__DynamicallyInvokable]
    public static void MemoryBarrier()
    {
      Thread.MemoryBarrier();
    }
  }
}
