// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.SafeHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Представляет класс-оболочку для дескрипторов операционной системы.
  ///    Этот класс должен наследоваться.
  /// </summary>
  [SecurityCritical]
  [__DynamicallyInvokable]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public abstract class SafeHandle : CriticalFinalizerObject, IDisposable
  {
    /// <summary>Определяет инкапсулируемый дескриптор.</summary>
    protected IntPtr handle;
    private int _state;
    private bool _ownsHandle;
    private bool _fullyInitialized;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.SafeHandle" /> с заданным значением недопустимого дескриптора.
    /// </summary>
    /// <param name="invalidHandleValue">
    ///   Значение недопустимого дескриптора (обычно 0 или -1).
    ///     Реализация <see cref="P:System.Runtime.InteropServices.SafeHandle.IsInvalid" /> должна возвращать <see langword="true" /> для этого значения.
    /// </param>
    /// <param name="ownsHandle">
    ///   Значение <see langword="true" />, если нужно надежно разрешить <see cref="T:System.Runtime.InteropServices.SafeHandle" /> освободить дескриптор на стадии завершения; в противном случае — значение <see langword="false" /> (не рекомендуется).
    /// </param>
    /// <exception cref="T:System.TypeLoadException">
    ///   Производный класс находится в сборке без разрешения на доступ к неуправляемому коду.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    protected SafeHandle(IntPtr invalidHandleValue, bool ownsHandle)
    {
      this.handle = invalidHandleValue;
      this._state = 4;
      this._ownsHandle = ownsHandle;
      if (!ownsHandle)
        GC.SuppressFinalize((object) this);
      this._fullyInitialized = true;
    }

    /// <summary>Освобождает все ресурсы, связанные с дескриптором.</summary>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    ~SafeHandle()
    {
      this.Dispose(false);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void InternalFinalize();

    /// <summary>
    ///   Определяет дескриптор для заданного ранее существующего дескриптора.
    /// </summary>
    /// <param name="handle">
    ///   Ранее существующий дескриптор для использования.
    /// </param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    protected void SetHandle(IntPtr handle)
    {
      this.handle = handle;
    }

    /// <summary>
    ///   Возвращает значение поля <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" />.
    /// </summary>
    /// <returns>
    ///   Указатель <see langword="IntPtr" />, представляющий значение поля <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" />.
    ///    Если дескриптор был помечен как недопустимый с помощью <see cref="M:System.Runtime.InteropServices.SafeHandle.SetHandleAsInvalid" />, этот метод, тем не менее, возвращает исходное значение дескриптора, которое может быть устаревшим.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public IntPtr DangerousGetHandle()
    {
      return this.handle;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, является ли дескриптор закрытым.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если дескриптор закрыт, в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsClosed
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get
      {
        return (this._state & 1) == 1;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает значение, показывающее, допустимо ли значение дескриптора.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если значение дескриптора является неправильным; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool IsInvalid { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), __DynamicallyInvokable] get; }

    /// <summary>
    ///   Помечает дескриптор для освобождения самого дескриптора и соответствующих ресурсов.
    /// </summary>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public void Close()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые классом <see cref="T:System.Runtime.InteropServices.SafeHandle" />.
    /// </summary>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые классом <see cref="T:System.Runtime.InteropServices.SafeHandle" />, определяя, нужно ли выполнять обычную операцию удаления.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> для обычной операции удаления и значение <see langword="false" /> для завершения работы с дескриптором.
    /// </param>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
        this.InternalDispose();
      else
        this.InternalFinalize();
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void InternalDispose();

    /// <summary>Помечает дескриптор как больше не используемый.</summary>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern void SetHandleAsInvalid();

    /// <summary>
    ///   При переопределении в производном классе выполняет код, необходимый для освобождения дескриптора.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если дескриптор освобождается успешно; в противном случае при катастрофическом сбое — значение <see langword=" false" />.
    ///    В таком случае создается управляемый помощник по отладке releaseHandleFailed MDA.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    protected abstract bool ReleaseHandle();

    /// <summary>
    ///   Вручную увеличивает счетчик ссылок для экземпляров <see cref="T:System.Runtime.InteropServices.SafeHandle" />.
    /// </summary>
    /// <param name="success">
    ///   Значение <see langword="true" />, если счетчик ссылок был успешно увеличен; в противном случае — значение <see langword="false" />.
    /// </param>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern void DangerousAddRef(ref bool success);

    /// <summary>
    ///   Вручную уменьшает счетчик ссылок для экземпляра <see cref="T:System.Runtime.InteropServices.SafeHandle" />.
    /// </summary>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern void DangerousRelease();
  }
}
