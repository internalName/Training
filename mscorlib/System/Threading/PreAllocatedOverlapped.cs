// Decompiled with JetBrains decompiler
// Type: System.Threading.PreAllocatedOverlapped
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Представляет предварительно выделенное состояние для собственных перекрывающихся операций ввода-вывода.
  /// </summary>
  public sealed class PreAllocatedOverlapped : IDisposable, IDeferredDisposable
  {
    [SecurityCritical]
    internal readonly ThreadPoolBoundHandleOverlapped _overlapped;
    private DeferredDisposableLifetime<PreAllocatedOverlapped> _lifetime;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.PreAllocatedOverlapped" /> и указывает делегат, вызываемый по завершении каждой асинхронной операции ввода-вывода, предоставляемый пользователем объект, предоставляющий контекст, и управляемые объекты, которые служат в качестве буфера.
    /// </summary>
    /// <param name="callback">
    ///   Делегат, который представляет метод обратного вызова, вызываемый после завершения каждой асинхронной операции ввода-вывода.
    /// </param>
    /// <param name="state">
    ///   Предоставляемый пользователем объект, отличающий экземпляр <see cref="T:System.Threading.NativeOverlapped" />, полученный из этого объекта, от других экземпляров <see cref="T:System.Threading.NativeOverlapped" />.
    ///    Это значение может быть равно <see langword="null" />.
    /// </param>
    /// <param name="pinData">
    ///   Объект или массив объектов, представляющих входной или выходной буфер для операций.
    ///    Каждый объект представляет буфер, такой массив байтов.
    ///    Это значение может быть равно <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="callback" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Этот метод был вызван после удаления <see cref="T:System.Threading.ThreadPoolBoundHandle" />.
    /// </exception>
    [CLSCompliant(false)]
    [SecuritySafeCritical]
    public PreAllocatedOverlapped(IOCompletionCallback callback, object state, object pinData)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof (callback));
      this._overlapped = new ThreadPoolBoundHandleOverlapped(callback, state, pinData, this);
    }

    internal bool AddRef()
    {
      return this._lifetime.AddRef(this);
    }

    [SecurityCritical]
    internal void Release()
    {
      this._lifetime.Release(this);
    }

    /// <summary>
    ///   Освобождает ресурсы, связанные с данным экземпляром <see cref="T:System.Threading.PreAllocatedOverlapped" />.
    /// </summary>
    public void Dispose()
    {
      this._lifetime.Dispose(this);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, прежде чем текущий экземпляр будет удален при сборке мусора.
    /// </summary>
    ~PreAllocatedOverlapped()
    {
      if (Environment.HasShutdownStarted)
        return;
      this.Dispose();
    }

    [SecurityCritical]
    void IDeferredDisposable.OnFinalRelease(bool disposed)
    {
      // ISSUE: unable to decompile the method.
    }
  }
}
