// Decompiled with JetBrains decompiler
// Type: System.Threading.AsyncLocalValueChangedArgs`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  /// <summary>
  ///   Класс, предоставляющий сведения об изменениях данных экземплярам <see cref="T:System.Threading.AsyncLocal`1" />, которые зарегистрированы для получения уведомлений об изменениях.
  /// </summary>
  /// <typeparam name="T">Тип данных.</typeparam>
  [__DynamicallyInvokable]
  public struct AsyncLocalValueChangedArgs<T>
  {
    /// <summary>Получает предыдущее значение данных.</summary>
    /// <returns>Предыдущее значение данных.</returns>
    [__DynamicallyInvokable]
    public T PreviousValue { [__DynamicallyInvokable] get; private set; }

    /// <summary>Получает текущее значение данных.</summary>
    /// <returns>Текущее значение данных.</returns>
    [__DynamicallyInvokable]
    public T CurrentValue { [__DynamicallyInvokable] get; private set; }

    /// <summary>
    ///   Возвращает значение, указывающее, изменяется ли значение из-за изменения контекста выполнения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если значение изменено из-за изменения контекста выполнения; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool ThreadContextChanged { [__DynamicallyInvokable] get; private set; }

    internal AsyncLocalValueChangedArgs(T previousValue, T currentValue, bool contextChanged)
    {
      this = new AsyncLocalValueChangedArgs<T>();
      this.PreviousValue = previousValue;
      this.CurrentValue = currentValue;
      this.ThreadContextChanged = contextChanged;
    }
  }
}
