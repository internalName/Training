// Decompiled with JetBrains decompiler
// Type: System.IDisposable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Предоставляет механизм для освобождения неуправляемых ресурсов.
  /// 
  ///   Для просмотра исходного кода .NET Framework для этого типа, в разделе Reference Source.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface IDisposable
  {
    /// <summary>
    ///   Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
    /// </summary>
    [__DynamicallyInvokable]
    void Dispose();
  }
}
