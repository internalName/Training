// Decompiled with JetBrains decompiler
// Type: System.Threading.AsyncFlowControl
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет функциональность для восстановления миграции или перемещения контекста выполнения между потоками.
  /// </summary>
  public struct AsyncFlowControl : IDisposable
  {
    private bool useEC;
    private ExecutionContext _ec;
    private SecurityContext _sc;
    private Thread _thread;

    [SecurityCritical]
    internal void Setup(SecurityContextDisableFlow flags)
    {
      this.useEC = false;
      Thread currentThread = Thread.CurrentThread;
      this._sc = currentThread.GetMutableExecutionContext().SecurityContext;
      this._sc._disableFlow = flags;
      this._thread = currentThread;
    }

    [SecurityCritical]
    internal void Setup()
    {
      this.useEC = true;
      Thread currentThread = Thread.CurrentThread;
      this._ec = currentThread.GetMutableExecutionContext();
      this._ec.isFlowSuppressed = true;
      this._thread = currentThread;
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Threading.AsyncFlowControl" />.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="T:System.Threading.AsyncFlowControl" /> Структура не используется в потоке которой он был создан.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.AsyncFlowControl" /> Структура уже используется для вызова <see cref="M:System.Threading.AsyncFlowControl.Dispose" /> или <see cref="M:System.Threading.AsyncFlowControl.Undo" />.
    /// </exception>
    public void Dispose()
    {
      this.Undo();
    }

    /// <summary>
    ///   Восстанавливает перемещение контекста выполнения между потоками.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="T:System.Threading.AsyncFlowControl" /> Структура не используется в потоке которой он был создан.
    /// 
    ///   -или-
    /// 
    ///   <see cref="T:System.Threading.AsyncFlowControl" /> Структура уже используется для вызова <see cref="M:System.Threading.AsyncFlowControl.Dispose" /> или <see cref="M:System.Threading.AsyncFlowControl.Undo" />.
    /// </exception>
    [SecuritySafeCritical]
    public void Undo()
    {
      if (this._thread == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseAFCMultiple"));
      if (this._thread != Thread.CurrentThread)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseAFCOtherThread"));
      if (this.useEC)
      {
        if (Thread.CurrentThread.GetMutableExecutionContext() != this._ec)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncFlowCtrlCtxMismatch"));
        ExecutionContext.RestoreFlow();
      }
      else
      {
        if (!Thread.CurrentThread.GetExecutionContextReader().SecurityContext.IsSame(this._sc))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncFlowCtrlCtxMismatch"));
        SecurityContext.RestoreFlow();
      }
      this._thread = (Thread) null;
    }

    /// <summary>
    ///   Возвращает хэш-код для текущего <see cref="T:System.Threading.AsyncFlowControl" /> структуры.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего <see cref="T:System.Threading.AsyncFlowControl" /> структуры.
    /// </returns>
    public override int GetHashCode()
    {
      if (this._thread != null)
        return this._thread.GetHashCode();
      return this.ToString().GetHashCode();
    }

    /// <summary>
    ///   Определяет, является ли заданный объект текущему объекту <see cref="T:System.Threading.AsyncFlowControl" /> структуры.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с текущей структурой.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является <see cref="T:System.Threading.AsyncFlowControl" /> структуры и равен текущему объекту <see cref="T:System.Threading.AsyncFlowControl" /> структуры; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is AsyncFlowControl)
        return this.Equals((AsyncFlowControl) obj);
      return false;
    }

    /// <summary>
    ///   Определяет ли указанный <see cref="T:System.Threading.AsyncFlowControl" /> структуры равен текущему объекту <see cref="T:System.Threading.AsyncFlowControl" /> структуры.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Threading.AsyncFlowControl" /> Структура для сравнения с текущей структурой.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> равно текущему <see cref="T:System.Threading.AsyncFlowControl" /> структуры; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(AsyncFlowControl obj)
    {
      if (obj.useEC == this.useEC && obj._ec == this._ec && obj._sc == this._sc)
        return obj._thread == this._thread;
      return false;
    }

    /// <summary>
    ///   Сравнивает два <see cref="T:System.Threading.AsyncFlowControl" /> структур с целью определить, равны ли они.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Threading.AsyncFlowControl" /> Структуры.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Threading.AsyncFlowControl" /> Структуры.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если обе структуры эквивалентны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(AsyncFlowControl a, AsyncFlowControl b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Сравнивает два <see cref="T:System.Threading.AsyncFlowControl" /> структуры, чтобы определить, не равны ли они.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Threading.AsyncFlowControl" /> Структуры.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Threading.AsyncFlowControl" /> Структуры.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если структуры не равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator !=(AsyncFlowControl a, AsyncFlowControl b)
    {
      return !(a == b);
    }
  }
}
