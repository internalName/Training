// Decompiled with JetBrains decompiler
// Type: System.Threading.AsyncLocal`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Представляет внешние данные, локальные для данного асинхронного потока управления, такие как асинхронный метод.
  /// </summary>
  /// <typeparam name="T">Тип внешних данных.</typeparam>
  [__DynamicallyInvokable]
  public sealed class AsyncLocal<T> : IAsyncLocal
  {
    [SecurityCritical]
    private readonly Action<AsyncLocalValueChangedArgs<T>> m_valueChangedHandler;

    /// <summary>
    ///   Создает экземпляр экземпляра <see cref="T:System.Threading.AsyncLocal`1" />, который не получает уведомления об изменениях.
    /// </summary>
    [__DynamicallyInvokable]
    public AsyncLocal()
    {
    }

    /// <summary>
    ///   Создает экземпляр локального экземпляра <see cref="T:System.Threading.AsyncLocal`1" />, который получает уведомления об изменениях.
    /// </summary>
    /// <param name="valueChangedHandler">
    ///   Делегат, который вызывается при каждом изменении текущего значения в любом потоке.
    /// </param>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public AsyncLocal(Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
    {
      this.m_valueChangedHandler = valueChangedHandler;
    }

    /// <summary>Получает или задает значение внешних данных.</summary>
    /// <returns>Значение внешних данных.</returns>
    [__DynamicallyInvokable]
    public T Value
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        object localValue = ExecutionContext.GetLocalValue((IAsyncLocal) this);
        if (localValue != null)
          return (T) localValue;
        return default (T);
      }
      [SecuritySafeCritical, __DynamicallyInvokable] set
      {
        ExecutionContext.SetLocalValue((IAsyncLocal) this, (object) value, this.m_valueChangedHandler != null);
      }
    }

    [SecurityCritical]
    void IAsyncLocal.OnValueChanged(object previousValueObj, object currentValueObj, bool contextChanged)
    {
      this.m_valueChangedHandler(new AsyncLocalValueChangedArgs<T>(previousValueObj == null ? default (T) : (T) previousValueObj, currentValueObj == null ? default (T) : (T) currentValueObj, contextChanged));
    }
  }
}
