// Decompiled with JetBrains decompiler
// Type: System.Threading.Overlapped
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет управляемое представление структуры OVERLAPPED Win32, включая методы для передачи данных из экземпляра <see cref="T:System.Threading.Overlapped" /> в структуру <see cref="T:System.Threading.NativeOverlapped" />.
  /// </summary>
  [ComVisible(true)]
  public class Overlapped
  {
    private static PinnableBufferCache s_overlappedDataCache = new PinnableBufferCache("System.Threading.OverlappedData", (Func<object>) (() => (object) new OverlappedData()));
    private OverlappedData m_overlappedData;

    /// <summary>
    ///   Инициализирует новый пустой экземпляр класса <see cref="T:System.Threading.Overlapped" /> класса.
    /// </summary>
    public Overlapped()
    {
      this.m_overlappedData = (OverlappedData) Overlapped.s_overlappedDataCache.Allocate();
      this.m_overlappedData.m_overlapped = this;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.Overlapped" /> положение класса с указанным файлом, дескриптор события, получает сигнал при завершении операции ввода-вывода и интерфейсом, через который возвращаются результаты операции.
    /// </summary>
    /// <param name="offsetLo">
    ///   Младшее слово позиции файла, с которого начинается перемещение данных.
    /// </param>
    /// <param name="offsetHi">
    ///   Старшее слово позиции файла, с которого начинается перемещение данных.
    /// </param>
    /// <param name="hEvent">
    ///   Дескриптор события, который отправляется сигнал при завершении операции ввода-вывода.
    /// </param>
    /// <param name="ar">
    ///   Объект, реализующий интерфейс <see cref="T:System.IAsyncResult" /> интерфейс и предоставляет сведения о состоянии операции ввода-вывода.
    /// </param>
    public Overlapped(int offsetLo, int offsetHi, IntPtr hEvent, IAsyncResult ar)
    {
      this.m_overlappedData = (OverlappedData) Overlapped.s_overlappedDataCache.Allocate();
      this.m_overlappedData.m_overlapped = this;
      this.m_overlappedData.m_nativeOverlapped.OffsetLow = offsetLo;
      this.m_overlappedData.m_nativeOverlapped.OffsetHigh = offsetHi;
      this.m_overlappedData.UserHandle = hEvent;
      this.m_overlappedData.m_asyncResult = ar;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.Overlapped" /> положение класса с указанным файлом, дескриптор 32-разрядное целое число, получает сигнал при завершении операции ввода-вывода и интерфейсом, через который возвращаются результаты операции.
    /// </summary>
    /// <param name="offsetLo">
    ///   Младшее слово позиции файла, с которого начинается перемещение данных.
    /// </param>
    /// <param name="offsetHi">
    ///   Старшее слово позиции файла, с которого начинается перемещение данных.
    /// </param>
    /// <param name="hEvent">
    ///   Дескриптор события, который отправляется сигнал при завершении операции ввода-вывода.
    /// </param>
    /// <param name="ar">
    ///   Объект, реализующий интерфейс <see cref="T:System.IAsyncResult" /> интерфейс и предоставляет сведения о состоянии операции ввода-вывода.
    /// </param>
    [Obsolete("This constructor is not 64-bit compatible.  Use the constructor that takes an IntPtr for the event handle.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public Overlapped(int offsetLo, int offsetHi, int hEvent, IAsyncResult ar)
      : this(offsetLo, offsetHi, new IntPtr(hEvent), ar)
    {
    }

    /// <summary>
    ///   Возвращает или задает объект, предоставляющий сведения о состоянии операции ввода-вывода.
    /// </summary>
    /// <returns>
    ///   Объект, реализующий интерфейс <see cref="T:System.IAsyncResult" />.
    /// </returns>
    public IAsyncResult AsyncResult
    {
      get
      {
        return this.m_overlappedData.m_asyncResult;
      }
      set
      {
        this.m_overlappedData.m_asyncResult = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает младшее слово позиции файла, с которого начинается перемещение данных.
    ///    Позиция в файле — это смещение в байтах от начала файла.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Int32" /> Значение, представляющее младшее слово позиции файла.
    /// </returns>
    public int OffsetLow
    {
      get
      {
        return this.m_overlappedData.m_nativeOverlapped.OffsetLow;
      }
      set
      {
        this.m_overlappedData.m_nativeOverlapped.OffsetLow = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает старшее слово позиции файла, с которой начинается передача данных.
    ///    Позиция в файле — это смещение в байтах от начала файла.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Int32" /> Значение, представляющее старшее слово позиции файла.
    /// </returns>
    public int OffsetHigh
    {
      get
      {
        return this.m_overlappedData.m_nativeOverlapped.OffsetHigh;
      }
      set
      {
        this.m_overlappedData.m_nativeOverlapped.OffsetHigh = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает дескриптор 32-разрядное целое число для события синхронизации, который отправляется сигнал при завершении операции ввода-вывода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Int32" /> Значение, представляющее дескриптор события синхронизации.
    /// </returns>
    [Obsolete("This property is not 64-bit compatible.  Use EventHandleIntPtr instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public int EventHandle
    {
      get
      {
        return this.m_overlappedData.UserHandle.ToInt32();
      }
      set
      {
        this.m_overlappedData.UserHandle = new IntPtr(value);
      }
    }

    /// <summary>
    ///   Возвращает или задает дескриптор для события синхронизации, которое получает сигнал при завершении операции ввода-вывода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.IntPtr" /> Представляет дескриптор события.
    /// </returns>
    [ComVisible(false)]
    public IntPtr EventHandleIntPtr
    {
      get
      {
        return this.m_overlappedData.UserHandle;
      }
      set
      {
        this.m_overlappedData.UserHandle = value;
      }
    }

    internal _IOCompletionCallback iocbHelper
    {
      get
      {
        return this.m_overlappedData.m_iocbHelper;
      }
    }

    internal IOCompletionCallback UserCallback
    {
      [SecurityCritical] get
      {
        return this.m_overlappedData.m_iocb;
      }
    }

    /// <summary>
    ///   Помещает текущий экземпляр в <see cref="T:System.Threading.NativeOverlapped" /> структуру, определяющую делегата, вызываемого при завершении асинхронной операции ввода-вывода.
    /// </summary>
    /// <param name="iocb">
    ///   <see cref="T:System.Threading.IOCompletionCallback" /> Делегат, представляющий метод обратного вызова вызывается при завершении асинхронной операции ввода-вывода.
    /// </param>
    /// <returns>
    ///   Неуправляемый указатель на <see cref="T:System.Threading.NativeOverlapped" /> структуры.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий <see cref="T:System.Threading.Overlapped" /> уже упакован.
    /// </exception>
    [SecurityCritical]
    [Obsolete("This method is not safe.  Use Pack (iocb, userData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    [CLSCompliant(false)]
    public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb)
    {
      return this.Pack(iocb, (object) null);
    }

    /// <summary>
    ///   Помещает текущий экземпляр в <see cref="T:System.Threading.NativeOverlapped" /> структуру, указав делегат, вызываемый при завершении асинхронной операции ввода-вывода и управляемый объект, который служит буфером.
    /// </summary>
    /// <param name="iocb">
    ///   <see cref="T:System.Threading.IOCompletionCallback" /> Делегат, представляющий метод обратного вызова вызывается при завершении асинхронной операции ввода-вывода.
    /// </param>
    /// <param name="userData">
    ///   Объект или массив объектов, представляющих входного или выходного буфера для операции.
    ///    Каждый объект представляет буфер, например в массив байтов.
    /// </param>
    /// <returns>
    ///   Неуправляемый указатель на <see cref="T:System.Threading.NativeOverlapped" /> структуры.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий <see cref="T:System.Threading.Overlapped" /> уже упакован.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
    {
      return this.m_overlappedData.Pack(iocb, userData);
    }

    /// <summary>
    ///   Помещает текущий экземпляр в <see cref="T:System.Threading.NativeOverlapped" /> Структура делегат, вызываемый при завершении асинхронной операции ввода-вывода.
    ///    Не распространяет вызывающий стек.
    /// </summary>
    /// <param name="iocb">
    ///   <see cref="T:System.Threading.IOCompletionCallback" /> Делегат, представляющий метод обратного вызова вызывается при завершении асинхронной операции ввода-вывода.
    /// </param>
    /// <returns>
    ///   Неуправляемый указатель на <see cref="T:System.Threading.NativeOverlapped" /> структуры.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий <see cref="T:System.Threading.Overlapped" /> уже упакован.
    /// </exception>
    [SecurityCritical]
    [Obsolete("This method is not safe.  Use UnsafePack (iocb, userData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    [CLSCompliant(false)]
    public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb)
    {
      return this.UnsafePack(iocb, (object) null);
    }

    /// <summary>
    ///   Помещает текущий экземпляр в <see cref="T:System.Threading.NativeOverlapped" /> структуру, указав делегат, вызываемый при завершении асинхронной операции ввода-вывода, и управляемый объект, который служит буфером.
    ///    Не распространяет вызывающий стек.
    /// </summary>
    /// <param name="iocb">
    ///   <see cref="T:System.Threading.IOCompletionCallback" /> Делегат, представляющий метод обратного вызова вызывается при завершении асинхронной операции ввода-вывода.
    /// </param>
    /// <param name="userData">
    ///   Объект или массив объектов, представляющих входного или выходного буфера для операции.
    ///    Каждый объект представляет буфер, например в массив байтов.
    /// </param>
    /// <returns>
    ///   Неуправляемый указатель на <see cref="T:System.Threading.NativeOverlapped" /> структуры.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий <see cref="T:System.Threading.Overlapped" /> уже упакованы.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    [ComVisible(false)]
    public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
    {
      return this.m_overlappedData.UnsafePack(iocb, userData);
    }

    /// <summary>
    ///   Распаковывает указанный неуправляемый <see cref="T:System.Threading.NativeOverlapped" /> структуру в управляемом <see cref="T:System.Threading.Overlapped" /> объекта.
    /// </summary>
    /// <param name="nativeOverlappedPtr">
    ///   Неуправляемый указатель на <see cref="T:System.Threading.NativeOverlapped" /> структуры.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Threading.Overlapped" /> Объект содержит сведения о распаковываются из собственного структуры.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="nativeOverlappedPtr" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public static unsafe Overlapped Unpack(NativeOverlapped* nativeOverlappedPtr)
    {
      if ((IntPtr) nativeOverlappedPtr == IntPtr.Zero)
        throw new ArgumentNullException(nameof (nativeOverlappedPtr));
      return OverlappedData.GetOverlappedFromNative(nativeOverlappedPtr).m_overlapped;
    }

    /// <summary>
    ///   Освобождает неуправляемые памяти, связанной с собственного перекрывающуюся структуру, выделяемая <see cref="Overload:System.Threading.Overlapped.Pack" /> метод.
    /// </summary>
    /// <param name="nativeOverlappedPtr">
    ///   Указатель на <see cref="T:System.Threading.NativeOverlapped" /> структуры должен быть освобожден.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="nativeOverlappedPtr" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public static unsafe void Free(NativeOverlapped* nativeOverlappedPtr)
    {
      if ((IntPtr) nativeOverlappedPtr == IntPtr.Zero)
        throw new ArgumentNullException(nameof (nativeOverlappedPtr));
      Overlapped overlapped = OverlappedData.GetOverlappedFromNative(nativeOverlappedPtr).m_overlapped;
      OverlappedData.FreeNativeOverlapped(nativeOverlappedPtr);
      OverlappedData overlappedData = overlapped.m_overlappedData;
      overlapped.m_overlappedData = (OverlappedData) null;
      overlappedData.ReInitialize();
      Overlapped.s_overlappedDataCache.Free((object) overlappedData);
    }
  }
}
