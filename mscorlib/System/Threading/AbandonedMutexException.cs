// Decompiled with JetBrains decompiler
// Type: System.Threading.AbandonedMutexException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
  /// <summary>
  ///   Исключение, которое вызывается, когда некоторый поток получает <see cref="T:System.Threading.Mutex" /> объекта, Брошенный другим потоком путем выхода без высвобождения.
  /// </summary>
  [ComVisible(false)]
  [__DynamicallyInvokable]
  [Serializable]
  public class AbandonedMutexException : SystemException
  {
    private int m_MutexIndex = -1;
    private Mutex m_Mutex;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.AbandonedMutexException" /> со значениями по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public AbandonedMutexException()
      : base(Environment.GetResourceString("Threading.AbandonedMutexException"))
    {
      this.SetErrorCode(-2146233043);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.AbandonedMutexException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке с объяснением причины исключения.
    /// </param>
    [__DynamicallyInvokable]
    public AbandonedMutexException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233043);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.AbandonedMutexException" /> класса с указанной ошибкой сообщением и внутренним исключением.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке с объяснением причины исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public AbandonedMutexException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233043);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.AbandonedMutexException" /> класса с указанным индексом для Брошенный mutex, если применимо и <see cref="T:System.Threading.Mutex" /> объект, представляющий мьютекс.
    /// </summary>
    /// <param name="location">
    ///   Индекс в массиве ожидания Брошенный mutex обрабатывает возникновение исключения для <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> метода или -1, если исключения для <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> или <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> методы.
    /// </param>
    /// <param name="handle">
    ///   Объект <see cref="T:System.Threading.Mutex" /> представляющий Брошенный mutex.
    /// </param>
    [__DynamicallyInvokable]
    public AbandonedMutexException(int location, WaitHandle handle)
      : base(Environment.GetResourceString("Threading.AbandonedMutexException"))
    {
      this.SetErrorCode(-2146233043);
      this.SetupException(location, handle);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.AbandonedMutexException" /> класса с указанной ошибкой сообщения, индекс Брошенный mutex, если применимо и Брошенный mutex.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке с объяснением причины исключения.
    /// </param>
    /// <param name="location">
    ///   Индекс в массиве ожидания Брошенный mutex обрабатывает возникновение исключения для <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> метода или -1, если исключения для <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> или <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> методы.
    /// </param>
    /// <param name="handle">
    ///   Объект <see cref="T:System.Threading.Mutex" /> представляющий Брошенный mutex.
    /// </param>
    [__DynamicallyInvokable]
    public AbandonedMutexException(string message, int location, WaitHandle handle)
      : base(message)
    {
      this.SetErrorCode(-2146233043);
      this.SetupException(location, handle);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.AbandonedMutexException" /> класса с указанным сообщением об ошибке, внутренним исключением, индекс Брошенный mutex, если применимо и <see cref="T:System.Threading.Mutex" /> объект, представляющий мьютекс.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке с объяснением причины исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    /// <param name="location">
    ///   Индекс в массиве ожидания Брошенный mutex обрабатывает возникновение исключения для <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> метода или -1, если исключения для <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> или <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> методы.
    /// </param>
    /// <param name="handle">
    ///   Объект <see cref="T:System.Threading.Mutex" /> представляющий Брошенный mutex.
    /// </param>
    [__DynamicallyInvokable]
    public AbandonedMutexException(string message, Exception inner, int location, WaitHandle handle)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233043);
      this.SetupException(location, handle);
    }

    private void SetupException(int location, WaitHandle handle)
    {
      this.m_MutexIndex = location;
      if (handle == null)
        return;
      this.m_Mutex = handle as Mutex;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.AbandonedMutexException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта по возникающему исключению.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected AbandonedMutexException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Получает брошенный mutex, вызвавшее исключение, если оно известно.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.Mutex" /> представляющий Брошенный mutex или <see langword="null" /> Если не удается найти Брошенный mutex.
    /// </returns>
    [__DynamicallyInvokable]
    public Mutex Mutex
    {
      [__DynamicallyInvokable] get
      {
        return this.m_Mutex;
      }
    }

    /// <summary>
    ///   Получает индекс Брошенный mutex, вызвавшее исключение, если оно известно.
    /// </summary>
    /// <returns>
    ///   Индекс в массиве дескрипторов ожидания передан <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> метода из <see cref="T:System.Threading.Mutex" /> представляющий Брошенный mutex, или – 1, если не удается определить индекс Брошенный mutex.
    /// </returns>
    [__DynamicallyInvokable]
    public int MutexIndex
    {
      [__DynamicallyInvokable] get
      {
        return this.m_MutexIndex;
      }
    }
  }
}
