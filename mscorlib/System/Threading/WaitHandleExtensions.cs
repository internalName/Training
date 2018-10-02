// Decompiled with JetBrains decompiler
// Type: System.Threading.WaitHandleExtensions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32.SafeHandles;
using System.Security;

namespace System.Threading
{
  /// <summary>
  ///   Предоставляет удобные методы для работы с безопасным дескриптором для дескриптора ожидания.
  /// </summary>
  [__DynamicallyInvokable]
  public static class WaitHandleExtensions
  {
    /// <summary>
    ///   Возвращает безопасный дескриптор для собственного дескриптора ожидания операционной системы.
    /// </summary>
    /// <param name="waitHandle">
    ///   Собственный дескриптор операционной системы.
    /// </param>
    /// <returns>
    ///   Дескриптор ожидания безопасный дескриптор ожидания, являющийся оболочкой для исходной операционной системой.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="waitHandle" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static SafeWaitHandle GetSafeWaitHandle(this WaitHandle waitHandle)
    {
      if (waitHandle == null)
        throw new ArgumentNullException(nameof (waitHandle));
      return waitHandle.SafeWaitHandle;
    }

    /// <summary>
    ///   Задает безопасный дескриптор для собственного дескриптора ожидания операционной системы.
    /// </summary>
    /// <param name="waitHandle">
    ///   Дескриптор ожидания, который инкапсулирует объект конкретного операционной системы, ожидающий монопольного доступа к общему ресурсу.
    /// </param>
    /// <param name="value">
    ///   Безопасный дескриптор, инкапсулирующий дескриптор операционной системы.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="waitHandle" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void SetSafeWaitHandle(this WaitHandle waitHandle, SafeWaitHandle value)
    {
      if (waitHandle == null)
        throw new ArgumentNullException(nameof (waitHandle));
      waitHandle.SafeWaitHandle = value;
    }
  }
}
