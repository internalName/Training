// Decompiled with JetBrains decompiler
// Type: System.Threading.RegisteredWaitHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Представляет дескриптор, который был зарегистрирован при вызове <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" />.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class RegisteredWaitHandle : MarshalByRefObject
  {
    private RegisteredWaitHandleSafe internalRegisteredWait;

    internal RegisteredWaitHandle()
    {
      this.internalRegisteredWait = new RegisteredWaitHandleSafe();
    }

    internal void SetHandle(IntPtr handle)
    {
      this.internalRegisteredWait.SetHandle(handle);
    }

    [SecurityCritical]
    internal void SetWaitObject(WaitHandle waitObject)
    {
      this.internalRegisteredWait.SetWaitObject(waitObject);
    }

    /// <summary>
    ///   Отменяет зарегистрированную операцию ожидания выданные <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" /> метод.
    /// </summary>
    /// <param name="waitObject">
    ///   <see cref="T:System.Threading.WaitHandle" /> Сигнала.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если функция выполнена успешно; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public bool Unregister(WaitHandle waitObject)
    {
      return this.internalRegisteredWait.Unregister(waitObject);
    }
  }
}
