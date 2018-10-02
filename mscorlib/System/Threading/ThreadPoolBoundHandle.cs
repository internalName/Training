// Decompiled with JetBrains decompiler
// Type: System.Threading.ThreadPoolBoundHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Представляет дескриптор ввода-вывода, который привязан к системному пулу потоков и позволяет низкоуровневым компонентам получать уведомления для асинхронных операций ввода-вывода.
  /// </summary>
  public sealed class ThreadPoolBoundHandle : IDisposable
  {
    private const int E_HANDLE = -2147024890;
    private const int E_INVALIDARG = -2147024809;
    [SecurityCritical]
    private readonly SafeHandle _handle;
    private bool _isDisposed;

    [SecurityCritical]
    private ThreadPoolBoundHandle(SafeHandle handle)
    {
      this._handle = handle;
    }

    /// <summary>Получает связанный дескриптор операционной системы.</summary>
    /// <returns>
    ///   Объект, содержащий связанный дескриптор операционной системы.
    /// </returns>
    public SafeHandle Handle
    {
      [SecurityCritical] get
      {
        return this._handle;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Threading.ThreadPoolBoundHandle" /> для указанного дескриптора, который привязан к системному пулу потоков.
    /// </summary>
    /// <param name="handle">
    ///   Объект, содержащий дескриптор операционной системы.
    ///    Дескриптор должен быть открыт для перекрывающегося ввода-вывода в неуправляемом коде.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.ThreadPoolBoundHandle" /> для <paramref name="handle" />, который привязан к системному пулу.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="handle" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Объект <paramref name="handle" /> был удален.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="handle" /> не ссылается на допустимый дескриптор ввода-вывода.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="handle" /> ссылается на дескриптор, который не был открыт для перекрывающегося ввода-вывода.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="handle" /> ссылается на дескриптор, который уже привязан.
    /// </exception>
    [SecurityCritical]
    public static ThreadPoolBoundHandle BindHandle(SafeHandle handle)
    {
      if (handle == null)
        throw new ArgumentNullException(nameof (handle));
      if (!handle.IsClosed)
      {
        if (!handle.IsInvalid)
        {
          try
          {
            ThreadPool.BindHandle(handle);
          }
          catch (Exception ex)
          {
            if (ex.HResult == -2147024890)
              throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"), nameof (handle));
            if (ex.HResult == -2147024809)
              throw new ArgumentException(Environment.GetResourceString("Argument_AlreadyBoundOrSyncHandle"), nameof (handle));
            throw;
          }
          return new ThreadPoolBoundHandle(handle);
        }
      }
      throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"), nameof (handle));
    }

    /// <summary>
    ///   Возвращает неуправляемый указатель на структуру <see cref="T:System.Threading.NativeOverlapped" />, обозначая делегат, вызываемый после завершения асинхронной операции ввода-вывода, предоставляемый пользователем объект, предоставляющий контекст, и управляемые объекты, которые служат в качестве буфера.
    /// </summary>
    /// <param name="callback">
    ///   Делегат, который представляет метод обратного вызова, вызываемый после завершения асинхронной операции ввода-вывода.
    /// </param>
    /// <param name="state">
    ///   Предоставляемый пользователем объект, отличающий этот экземпляр <see cref="T:System.Threading.NativeOverlapped" /> от других экземпляров <see cref="T:System.Threading.NativeOverlapped" />.
    /// </param>
    /// <param name="pinData">
    ///   Объект или массив объектов, представляющих входной или выходной буфер для операции, или <see langword="null" />.
    ///    Каждый объект представляет буфер, такой массив байтов.
    /// </param>
    /// <returns>
    ///   Неуправляемый указатель на структуру <see cref="T:System.Threading.NativeOverlapped" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="callback" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Этот метод был вызван после удаления объекта <see cref="T:System.Threading.ThreadPoolBoundHandle" />.
    /// </exception>
    [CLSCompliant(false)]
    [SecurityCritical]
    public unsafe NativeOverlapped* AllocateNativeOverlapped(IOCompletionCallback callback, object state, object pinData)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof (callback));
      this.EnsureNotDisposed();
      return new ThreadPoolBoundHandleOverlapped(callback, state, pinData, (PreAllocatedOverlapped) null)
      {
        _boundHandle = this
      }._nativeOverlapped;
    }

    /// <summary>
    ///   Возвращает неуправляемый указатель на структуру <see cref="T:System.Threading.NativeOverlapped" /> с помощью состояния обратного вызова и буферов, связанных с указанным объектом <see cref="T:System.Threading.PreAllocatedOverlapped" />.
    /// </summary>
    /// <param name="preAllocated">
    ///   Объект, из которого создается указатель <see cref="T:System.Threading.NativeOverlapped" />.
    /// </param>
    /// <returns>
    ///   Неуправляемый указатель на структуру <see cref="T:System.Threading.NativeOverlapped" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="preAllocated" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="preAllocated" /> сейчас используется для другой операции ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Этот метод был вызван после удаления <see cref="T:System.Threading.ThreadPoolBoundHandle" />.
    /// 
    ///   -или-
    /// 
    ///   Этот метод был вызван после удаления <paramref name="preAllocated" />.
    /// </exception>
    [CLSCompliant(false)]
    [SecurityCritical]
    public unsafe NativeOverlapped* AllocateNativeOverlapped(PreAllocatedOverlapped preAllocated)
    {
      if (preAllocated == null)
        throw new ArgumentNullException(nameof (preAllocated));
      this.EnsureNotDisposed();
      preAllocated.AddRef();
      try
      {
        ThreadPoolBoundHandleOverlapped overlapped = preAllocated._overlapped;
        if (overlapped._boundHandle != null)
          throw new ArgumentException(Environment.GetResourceString("Argument_PreAllocatedAlreadyAllocated"), nameof (preAllocated));
        overlapped._boundHandle = this;
        return overlapped._nativeOverlapped;
      }
      catch
      {
        preAllocated.Release();
        throw;
      }
    }

    /// <summary>
    ///   Освобождает память, связанную со структурой <see cref="T:System.Threading.NativeOverlapped" />, выделенной с помощью метода <see cref="Overload:System.Threading.ThreadPoolBoundHandle.AllocateNativeOverlapped" />.
    /// </summary>
    /// <param name="overlapped">
    ///   Неуправляемый указатель на освобождаемую структуру <see cref="T:System.Threading.NativeOverlapped" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="overlapped" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Этот метод был вызван после удаления объекта <see cref="T:System.Threading.ThreadPoolBoundHandle" />.
    /// </exception>
    [CLSCompliant(false)]
    [SecurityCritical]
    public unsafe void FreeNativeOverlapped(NativeOverlapped* overlapped)
    {
      if ((IntPtr) overlapped == IntPtr.Zero)
        throw new ArgumentNullException(nameof (overlapped));
      ThreadPoolBoundHandleOverlapped overlappedWrapper = ThreadPoolBoundHandle.GetOverlappedWrapper(overlapped, this);
      if (overlappedWrapper._boundHandle != this)
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeOverlappedWrongBoundHandle"), nameof (overlapped));
      if (overlappedWrapper._preAllocated != null)
        overlappedWrapper._preAllocated.Release();
      else
        Overlapped.Free(overlapped);
    }

    /// <summary>
    ///   Возвращает предоставляемый пользователем объект, который был указан при выделении экземпляра <see cref="T:System.Threading.NativeOverlapped" /> путем вызова метода <see cref="M:System.Threading.ThreadPoolBoundHandle.AllocateNativeOverlapped(System.Threading.IOCompletionCallback,System.Object,System.Object)" />.
    /// </summary>
    /// <param name="overlapped">
    ///   Неуправляемый указатель на структуру <see cref="T:System.Threading.NativeOverlapped" />, из которой следует получить связанный предоставляемый пользователем объект.
    /// </param>
    /// <returns>
    ///   Предоставляемый пользователем объект, отличающий этот экземпляр <see cref="T:System.Threading.NativeOverlapped" /> от других экземпляров <see cref="T:System.Threading.NativeOverlapped" />, или <see langword="null" />, если объект не был указан при выделении экземпляра путем вызова метода <see cref="Overload:System.Threading.ThreadPoolBoundHandle.AllocateNativeOverlapped" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="overlapped" /> имеет значение <see langword="null" />.
    /// </exception>
    [CLSCompliant(false)]
    [SecurityCritical]
    public static unsafe object GetNativeOverlappedState(NativeOverlapped* overlapped)
    {
      if ((IntPtr) overlapped == IntPtr.Zero)
        throw new ArgumentNullException(nameof (overlapped));
      return ThreadPoolBoundHandle.GetOverlappedWrapper(overlapped, (ThreadPoolBoundHandle) null)._userState;
    }

    [SecurityCritical]
    private static unsafe ThreadPoolBoundHandleOverlapped GetOverlappedWrapper(NativeOverlapped* overlapped, ThreadPoolBoundHandle expectedBoundHandle)
    {
      try
      {
        return (ThreadPoolBoundHandleOverlapped) Overlapped.Unpack(overlapped);
      }
      catch (NullReferenceException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_NativeOverlappedAlreadyFree"), nameof (overlapped), (Exception) ex);
      }
    }

    /// <summary>
    ///   Высвобождает все неуправляемые ресурсы, используемые экземпляром <see cref="T:System.Threading.ThreadPoolBoundHandle" />.
    /// </summary>
    public void Dispose()
    {
      this._isDisposed = true;
    }

    private void EnsureNotDisposed()
    {
      if (this._isDisposed)
        throw new ObjectDisposedException(this.GetType().ToString());
    }
  }
}
