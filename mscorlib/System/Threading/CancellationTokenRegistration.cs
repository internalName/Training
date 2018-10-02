// Decompiled with JetBrains decompiler
// Type: System.Threading.CancellationTokenRegistration
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Представляет делегат обратного вызова, который был зарегистрирован с <see cref="T:System.Threading.CancellationToken" />.
  /// </summary>
  [__DynamicallyInvokable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  public struct CancellationTokenRegistration : IEquatable<CancellationTokenRegistration>, IDisposable
  {
    private readonly CancellationCallbackInfo m_callbackInfo;
    private readonly SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> m_registrationInfo;

    internal CancellationTokenRegistration(CancellationCallbackInfo callbackInfo, SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> registrationInfo)
    {
      this.m_callbackInfo = callbackInfo;
      this.m_registrationInfo = registrationInfo;
    }

    [FriendAccessAllowed]
    internal bool TryDeregister()
    {
      return this.m_registrationInfo.Source != null && this.m_registrationInfo.Source.SafeAtomicRemove(this.m_registrationInfo.Index, this.m_callbackInfo) == this.m_callbackInfo;
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Threading.CancellationTokenRegistration" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      bool flag = this.TryDeregister();
      CancellationCallbackInfo callbackInfo = this.m_callbackInfo;
      if (callbackInfo == null)
        return;
      CancellationTokenSource cancellationTokenSource = callbackInfo.CancellationTokenSource;
      if (!cancellationTokenSource.IsCancellationRequested || cancellationTokenSource.IsCancellationCompleted || (flag || cancellationTokenSource.ThreadIDExecutingCallbacks == Thread.CurrentThread.ManagedThreadId))
        return;
      cancellationTokenSource.WaitForCallbackToComplete(this.m_callbackInfo);
    }

    /// <summary>
    ///   Определяет, равны ли два экземпляра <see cref="T:System.Threading.CancellationTokenRegistration" />.
    /// </summary>
    /// <param name="left">Первый экземпляр.</param>
    /// <param name="right">Второй экземпляр.</param>
    /// <returns>
    ///   Значение true, если экземпляры равны; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(CancellationTokenRegistration left, CancellationTokenRegistration right)
    {
      return left.Equals(right);
    }

    /// <summary>
    ///   Определяет, действительно ли два экземпляра <see cref="T:System.Threading.CancellationTokenRegistration" /> не равны.
    /// </summary>
    /// <param name="left">Первый экземпляр.</param>
    /// <param name="right">Второй экземпляр.</param>
    /// <returns>
    ///   Значение true, если экземпляры не равны; в противном случае — значение false.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(CancellationTokenRegistration left, CancellationTokenRegistration right)
    {
      return !left.Equals(right);
    }

    /// <summary>
    ///   Определяет, равен ли текущий экземпляр <see cref="T:System.Threading.CancellationTokenRegistration" /> заданному объекту <see cref="T:System.Threading.CancellationTokenRegistration" />.
    /// </summary>
    /// <param name="obj">
    ///   Второй объект, с которым нужно сравнить данный экземпляр.
    /// </param>
    /// <returns>
    ///   Значение true, если этот и <paramref name="obj" /> равны.
    ///    Значение false, в противном случае.
    /// 
    ///   Два <see cref="T:System.Threading.CancellationTokenRegistration" /> они равны, если они ссылаются на выход одного вызова того же метода Register <see cref="T:System.Threading.CancellationToken" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj is CancellationTokenRegistration)
        return this.Equals((CancellationTokenRegistration) obj);
      return false;
    }

    /// <summary>
    ///   Определяет, равен ли текущий экземпляр <see cref="T:System.Threading.CancellationTokenRegistration" /> заданному объекту <see cref="T:System.Threading.CancellationTokenRegistration" />.
    /// </summary>
    /// <param name="other">
    ///   Второй токен <see cref="T:System.Threading.CancellationTokenRegistration" />, с которым нужно сравнить данный экземпляр.
    /// </param>
    /// <returns>
    ///   Значение true, если этот и <paramref name="other" /> равны.
    ///    Значение false, в противном случае.
    /// 
    ///    Два <see cref="T:System.Threading.CancellationTokenRegistration" /> они равны, если они ссылаются на выход одного вызова того же метода Register <see cref="T:System.Threading.CancellationToken" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Equals(CancellationTokenRegistration other)
    {
      if (this.m_callbackInfo == other.m_callbackInfo)
      {
        SparselyPopulatedArrayFragment<CancellationCallbackInfo> source1 = this.m_registrationInfo.Source;
        SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> registrationInfo = other.m_registrationInfo;
        SparselyPopulatedArrayFragment<CancellationCallbackInfo> source2 = registrationInfo.Source;
        if (source1 == source2)
        {
          registrationInfo = this.m_registrationInfo;
          int index1 = registrationInfo.Index;
          registrationInfo = other.m_registrationInfo;
          int index2 = registrationInfo.Index;
          return index1 == index2;
        }
      }
      return false;
    }

    /// <summary>
    ///   Служит хэш-функцией для <see cref="T:System.Threading.CancellationTokenRegistration" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего экземпляра <see cref="T:System.Threading.CancellationTokenRegistration" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      if (this.m_registrationInfo.Source != null)
        return this.m_registrationInfo.Source.GetHashCode() ^ this.m_registrationInfo.Index.GetHashCode();
      return this.m_registrationInfo.Index.GetHashCode();
    }
  }
}
