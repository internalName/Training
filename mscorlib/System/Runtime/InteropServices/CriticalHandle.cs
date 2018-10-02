// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CriticalHandle
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
  ///   Представляет класс-оболочку для ресурсов обработчика.
  /// </summary>
  [SecurityCritical]
  [__DynamicallyInvokable]
  [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
  public abstract class CriticalHandle : CriticalFinalizerObject, IDisposable
  {
    /// <summary>Определяет инкапсулируемый дескриптор.</summary>
    protected IntPtr handle;
    private bool _isClosed;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> с заданным значением недопустимого дескриптора.
    /// </summary>
    /// <param name="invalidHandleValue">
    ///   Значение недопустимого дескриптора (обычно 0 или -1).
    /// </param>
    /// <exception cref="T:System.TypeLoadException">
    ///   Производный класс находится в сборке без разрешения на доступ к неуправляемому коду.
    /// </exception>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    protected CriticalHandle(IntPtr invalidHandleValue)
    {
      this.handle = invalidHandleValue;
      this._isClosed = false;
    }

    /// <summary>Освобождает все ресурсы, связанные с дескриптором.</summary>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    ~CriticalHandle()
    {
      this.Dispose(false);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private void Cleanup()
    {
      if (this.IsClosed)
        return;
      this._isClosed = true;
      if (this.IsInvalid)
        return;
      int lastWin32Error = Marshal.GetLastWin32Error();
      if (!this.ReleaseHandle())
        this.FireCustomerDebugProbe();
      Marshal.SetLastWin32Error(lastWin32Error);
      GC.SuppressFinalize((object) this);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void FireCustomerDebugProbe();

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
        return this._isClosed;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает значение, показывающее, допустимо ли значение дескриптора.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если дескриптор действителен, в противном случае — значение <see langword="false" />.
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
    ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.Runtime.InteropServices.CriticalHandle" />.
    /// </summary>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые классом <see cref="T:System.Runtime.InteropServices.CriticalHandle" />, определяя, нужно ли выполнять обычную операцию удаления.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> для обычной операции удаления и значение <see langword="false" /> для завершения работы с дескриптором.
    /// </param>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      this.Cleanup();
    }

    /// <summary>Помечает дескриптор как недопустимый.</summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public void SetHandleAsInvalid()
    {
      this._isClosed = true;
      GC.SuppressFinalize((object) this);
    }

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
  }
}
