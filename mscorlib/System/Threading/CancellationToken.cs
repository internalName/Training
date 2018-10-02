// Decompiled with JetBrains decompiler
// Type: System.Threading.CancellationToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Распространяет уведомление о том, что операции следует отменить.
  /// </summary>
  [ComVisible(false)]
  [DebuggerDisplay("IsCancellationRequested = {IsCancellationRequested}")]
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct CancellationToken
  {
    private static readonly Action<object> s_ActionToActionObjShunt = new Action<object>(CancellationToken.ActionToActionObjShunt);
    private CancellationTokenSource m_source;

    /// <summary>
    ///   Возвращает пустое значение <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <returns>Пустой токен отмены.</returns>
    [__DynamicallyInvokable]
    public static CancellationToken None
    {
      [__DynamicallyInvokable] get
      {
        return new CancellationToken();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, есть ли для данного токена запрос на отмену.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если для данного токена есть запрос на отмену; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsCancellationRequested
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_source != null)
          return this.m_source.IsCancellationRequested;
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, может ли данный токен находиться в отмененном состоянии.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если данный токен может быть в отмененном состоянии; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool CanBeCanceled
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_source != null)
          return this.m_source.CanBeCanceled;
        return false;
      }
    }

    /// <summary>
    ///   Возвращает дескриптор <see cref="T:System.Threading.WaitHandle" />, получающий сигнал при отмене токена.
    /// </summary>
    /// <returns>
    ///   Дескриптор <see cref="T:System.Threading.WaitHandle" />, получающий сигнал при отмене токена.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Связанный объект <see cref="T:System.Threading.CancellationTokenSource" /> удален.
    /// </exception>
    [__DynamicallyInvokable]
    public WaitHandle WaitHandle
    {
      [__DynamicallyInvokable] get
      {
        if (this.m_source == null)
          this.InitializeDefaultSource();
        return this.m_source.WaitHandle;
      }
    }

    internal CancellationToken(CancellationTokenSource source)
    {
      this.m_source = source;
    }

    /// <summary>
    ///   Инициализирует объект <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="canceled">Состояние отмены для токена.</param>
    [__DynamicallyInvokable]
    public CancellationToken(bool canceled)
    {
      this = new CancellationToken();
      if (!canceled)
        return;
      this.m_source = CancellationTokenSource.InternalGetStaticSource(canceled);
    }

    private static void ActionToActionObjShunt(object obj)
    {
      (obj as Action)();
    }

    /// <summary>
    ///   Регистрирует делегат, который будет вызываться при отмене данного токена <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="callback">
    ///   Делегат, выполняемый при отмене токена <see cref="T:System.Threading.CancellationToken" />.
    /// </param>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Threading.CancellationTokenRegistration" />, который можно использовать для отмены регистрации обратного вызова.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Связанный объект <see cref="T:System.Threading.CancellationTokenSource" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="callback" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    public CancellationTokenRegistration Register(Action callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof (callback));
      return this.Register(CancellationToken.s_ActionToActionObjShunt, (object) callback, false, true);
    }

    /// <summary>
    ///   Регистрирует делегат, который будет вызываться при отмене данного токена <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="callback">
    ///   Делегат, выполняемый при отмене токена <see cref="T:System.Threading.CancellationToken" />.
    /// </param>
    /// <param name="useSynchronizationContext">
    ///   Значение, указывающее, следует ли записывать текущий объект <see cref="T:System.Threading.SynchronizationContext" /> и использовать его при вызове <paramref name="callback" />.
    /// </param>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Threading.CancellationTokenRegistration" />, который можно использовать для отмены регистрации обратного вызова.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Связанный объект <see cref="T:System.Threading.CancellationTokenSource" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="callback" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof (callback));
      return this.Register(CancellationToken.s_ActionToActionObjShunt, (object) callback, useSynchronizationContext, true);
    }

    /// <summary>
    ///   Регистрирует делегат, который будет вызываться при отмене данного токена <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="callback">
    ///   Делегат, выполняемый при отмене токена <see cref="T:System.Threading.CancellationToken" />.
    /// </param>
    /// <param name="state">
    ///   Состояние, передаваемое обратному вызову <paramref name="callback" /> при вызове делегата.
    ///    Может содержать пустое значение.
    /// </param>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Threading.CancellationTokenRegistration" />, который можно использовать для отмены регистрации обратного вызова.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Связанный объект <see cref="T:System.Threading.CancellationTokenSource" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="callback" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    public CancellationTokenRegistration Register(Action<object> callback, object state)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof (callback));
      return this.Register(callback, state, false, true);
    }

    /// <summary>
    ///   Регистрирует делегат, который будет вызываться при отмене данного токена <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="callback">
    ///   Делегат, выполняемый при отмене токена <see cref="T:System.Threading.CancellationToken" />.
    /// </param>
    /// <param name="state">
    ///   Состояние, передаваемое обратному вызову <paramref name="callback" /> при вызове делегата.
    ///    Может содержать пустое значение.
    /// </param>
    /// <param name="useSynchronizationContext">
    ///   Логическое значение, указывающее, следует ли записывать текущий объект <see cref="T:System.Threading.SynchronizationContext" /> и использовать его при вызове <paramref name="callback" />.
    /// </param>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Threading.CancellationTokenRegistration" />, который можно использовать для отмены регистрации обратного вызова.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Связанный объект <see cref="T:System.Threading.CancellationTokenSource" /> удален.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="callback" /> имеет значение NULL.
    /// </exception>
    [__DynamicallyInvokable]
    public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext)
    {
      return this.Register(callback, state, useSynchronizationContext, true);
    }

    internal CancellationTokenRegistration InternalRegisterWithoutEC(Action<object> callback, object state)
    {
      return this.Register(callback, state, false, false);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext, bool useExecutionContext)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      if (callback == null)
        throw new ArgumentNullException(nameof (callback));
      if (!this.CanBeCanceled)
        return new CancellationTokenRegistration();
      SynchronizationContext targetSyncContext = (SynchronizationContext) null;
      ExecutionContext executionContext = (ExecutionContext) null;
      if (!this.IsCancellationRequested)
      {
        if (useSynchronizationContext)
          targetSyncContext = SynchronizationContext.Current;
        if (useExecutionContext)
          executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.OptimizeDefaultCase);
      }
      return this.m_source.InternalRegister(callback, state, targetSyncContext, executionContext);
    }

    /// <summary>
    ///   Определяет, равен ли текущий экземпляр <see cref="T:System.Threading.CancellationToken" /> заданному токену.
    /// </summary>
    /// <param name="other">
    ///   Второй токен <see cref="T:System.Threading.CancellationToken" />, с которым нужно сравнить данный экземпляр.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если экземпляры равны; в противном случае — <see langword="false" />.
    ///    Два токена равны, если они связаны с одним <see cref="T:System.Threading.CancellationTokenSource" /> или если они оба были созданы из открытых конструкторов <see cref="T:System.Threading.CancellationToken" /> и их значения <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> равны.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(CancellationToken other)
    {
      if (this.m_source == null && other.m_source == null)
        return true;
      if (this.m_source == null)
        return other.m_source == CancellationTokenSource.InternalGetStaticSource(false);
      if (other.m_source == null)
        return this.m_source == CancellationTokenSource.InternalGetStaticSource(false);
      return this.m_source == other.m_source;
    }

    /// <summary>
    ///   Определяет, равен ли текущий экземпляр <see cref="T:System.Threading.CancellationToken" /> заданному объекту <see cref="T:System.Object" />.
    /// </summary>
    /// <param name="other">
    ///   Второй объект, с которым нужно сравнить данный экземпляр.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если параметр является <paramref name="other" /> имеет значение <see cref="T:System.Threading.CancellationToken" />, и если два эти экземпляра равны; в противном случае — значение <see langword="false" />.
    ///    Два токена равны, если они связаны с одним <see cref="T:System.Threading.CancellationTokenSource" />, или если они оба были созданы из открытых конструкторов <see cref="T:System.Threading.CancellationToken" />, и их значения <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> равны.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Связанный объект <see cref="T:System.Threading.CancellationTokenSource" /> был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public override bool Equals(object other)
    {
      if (other is CancellationToken)
        return this.Equals((CancellationToken) other);
      return false;
    }

    /// <summary>
    ///   Служит хэш-функцией для <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего экземпляра <see cref="T:System.Threading.CancellationToken" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      if (this.m_source == null)
        return CancellationTokenSource.InternalGetStaticSource(false).GetHashCode();
      return this.m_source.GetHashCode();
    }

    /// <summary>
    ///   Определяет, равны ли два экземпляра <see cref="T:System.Threading.CancellationToken" />.
    /// </summary>
    /// <param name="left">Первый экземпляр.</param>
    /// <param name="right">Второй экземпляр.</param>
    /// <returns>
    ///   <see langword="true" />, если экземпляры равны; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Связанный объект <see cref="T:System.Threading.CancellationTokenSource" /> был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool operator ==(CancellationToken left, CancellationToken right)
    {
      return left.Equals(right);
    }

    /// <summary>
    ///   Определяет, действительно ли два экземпляра <see cref="T:System.Threading.CancellationToken" /> не равны.
    /// </summary>
    /// <param name="left">Первый экземпляр.</param>
    /// <param name="right">Второй экземпляр.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если эти экземпляры не равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Связанный объект <see cref="T:System.Threading.CancellationTokenSource" /> был удален.
    /// </exception>
    [__DynamicallyInvokable]
    public static bool operator !=(CancellationToken left, CancellationToken right)
    {
      return !left.Equals(right);
    }

    /// <summary>
    ///   Создает исключение <see cref="T:System.OperationCanceledException" />, если для данного токена есть запрос на отмену.
    /// </summary>
    /// <exception cref="T:System.OperationCanceledException">
    ///   Этот токен имел запрос на отмену.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Связанный объект <see cref="T:System.Threading.CancellationTokenSource" /> удален.
    /// </exception>
    [__DynamicallyInvokable]
    public void ThrowIfCancellationRequested()
    {
      if (!this.IsCancellationRequested)
        return;
      this.ThrowOperationCanceledException();
    }

    internal void ThrowIfSourceDisposed()
    {
      if (this.m_source == null || !this.m_source.IsDisposed)
        return;
      CancellationToken.ThrowObjectDisposedException();
    }

    private void ThrowOperationCanceledException()
    {
      throw new OperationCanceledException(Environment.GetResourceString("OperationCanceled"), this);
    }

    private static void ThrowObjectDisposedException()
    {
      throw new ObjectDisposedException((string) null, Environment.GetResourceString("CancellationToken_SourceDisposed"));
    }

    private void InitializeDefaultSource()
    {
      this.m_source = CancellationTokenSource.InternalGetStaticSource(false);
    }
  }
}
