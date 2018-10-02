// Decompiled with JetBrains decompiler
// Type: System.IAsyncResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
  /// <summary>Представляет состояние асинхронной операции.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IAsyncResult
  {
    /// <summary>
    ///   Возвращает значение, указывающее, выполнена ли асинхронная операция.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если операция выполнена; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool IsCompleted { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает дескриптор <see cref="T:System.Threading.WaitHandle" />, используемый для ожидания завершения асинхронной операции.
    /// </summary>
    /// <returns>
    ///   Дескриптор <see cref="T:System.Threading.WaitHandle" />, используемый для ожидания завершения асинхронной операции.
    /// </returns>
    [__DynamicallyInvokable]
    WaitHandle AsyncWaitHandle { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Получает определенный пользователем объект, который определяет или содержит сведения об асинхронной операции.
    /// </summary>
    /// <returns>
    ///   Определенный пользователем объект, который определяет или содержит сведения об асинхронной операции.
    /// </returns>
    [__DynamicallyInvokable]
    object AsyncState { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает значение, указывающее, выполнялась ли асинхронная операция синхронно.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если асинхронная операция выполнена синхронно, в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    bool CompletedSynchronously { [__DynamicallyInvokable] get; }
  }
}
