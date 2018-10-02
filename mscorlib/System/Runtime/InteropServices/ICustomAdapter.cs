// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ICustomAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Позволяет клиентам получать доступ к фактическому объекту, а не к объекту адаптера, предоставленному пользовательским упаковщиком.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public interface ICustomAdapter
  {
    /// <summary>
    ///   Предоставляет доступ к базовому объекту, упакованному настраиваемым модулем.
    /// </summary>
    /// <returns>Объект, содержащийся в объекте адаптера.</returns>
    [__DynamicallyInvokable]
    [return: MarshalAs(UnmanagedType.IUnknown)]
    object GetUnderlyingObject();
  }
}
