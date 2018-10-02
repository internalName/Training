// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ICriticalNotifyCompletion
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Представляет объект типа awaiter, который планирует продолжение работы после завершения операции await.
  /// </summary>
  [__DynamicallyInvokable]
  public interface ICriticalNotifyCompletion : INotifyCompletion
  {
    /// <summary>
    ///   Планирование продолжения действия, который вызывается при завершении экземпляра.
    /// </summary>
    /// <param name="continuation">
    ///   Действие, вызываемый после завершения операции.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="continuation" /> Аргумент имеет значение null (Nothing в Visual Basic).
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    void UnsafeOnCompleted(Action continuation);
  }
}
