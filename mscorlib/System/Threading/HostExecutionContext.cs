// Decompiled with JetBrains decompiler
// Type: System.Threading.HostExecutionContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Инкапсулирует и распространяет контекст выполнения хоста по потокам.
  /// </summary>
  public class HostExecutionContext : IDisposable
  {
    private object state;

    /// <summary>
    ///   Возвращает или задает состояние контекста выполнения хоста.
    /// </summary>
    /// <returns>
    ///   Объект, представляющий состояние контекста выполнения хоста.
    /// </returns>
    protected internal object State
    {
      get
      {
        return this.state;
      }
      set
      {
        this.state = value;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.HostExecutionContext" />.
    /// </summary>
    public HostExecutionContext()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Threading.HostExecutionContext" /> класса с помощью указанного состояния.
    /// </summary>
    /// <param name="state">
    ///   Объект, представляющий состояние контекста выполнения хоста.
    /// </param>
    public HostExecutionContext(object state)
    {
      this.state = state;
    }

    /// <summary>Создает копию текущего контекста выполнения хоста.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Threading.HostExecutionContext" /> объект, представляющий контекст хост-сайта для текущего потока.
    /// </returns>
    [SecuritySafeCritical]
    public virtual HostExecutionContext CreateCopy()
    {
      object obj = this.state;
      if (this.state is IUnknownSafeHandle)
        obj = ((IUnknownSafeHandle) this.state).Clone();
      return new HostExecutionContext(this.state);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Threading.HostExecutionContext" />.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   При переопределении в производном классе освобождает неуправляемые ресурсы, используемые объектом <see cref="T:System.Threading.WaitHandle" />, и при необходимости освобождает управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    public virtual void Dispose(bool disposing)
    {
    }
  }
}
