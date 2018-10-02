// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IActivationFactory
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>
  ///   Включает классы активируемый Среда выполнения Windows.
  /// </summary>
  [Guid("00000035-0000-0000-C000-000000000046")]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IActivationFactory
  {
    /// <summary>
    ///   Возвращает новый экземпляр Среда выполнения Windows класс, созданный <see cref="T:System.Runtime.InteropServices.WindowsRuntime.IActivationFactory" /> интерфейса.
    /// </summary>
    /// <returns>Новый экземпляр Среда выполнения Windows класса.</returns>
    [__DynamicallyInvokable]
    object ActivateInstance();
  }
}
